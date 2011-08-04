using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using EyeTracker.DAL.Interfaces;
using EyeTracker.Common.Logger;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Text;

namespace EyeTracker.Models
{
    public static class HeatMapImage_
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        private const double WAVE_LENGTH_VIOLET = 350;
        private const double WAVE_LENGTH_BLUE_VIOLET = 420;
        private const double WAVE_LENGTH_BLUE = 440;
        private const double WAVE_LENGTH_BLUE_LIGHT = 490;
        private const double WAVE_LENGTH_GREEN = 510;
        private const double WAVE_LENGTH_YELLOW = 580;
        private const double WAVE_LENGTH_RED_YELLOW = 645;
        private const double WAVE_LENGTH_RED = 700;
        private const double WAVE_LENGTH_RED_VIOLET = 780;

        private const double WAVE_LENGTH_DIFF = WAVE_LENGTH_RED - WAVE_LENGTH_BLUE;

        private const float IMAGE_OPACITY = 0.5f;

        private static Color getColorFromWaveLength(double Wavelength)
        {
            double Gamma = 1.00;
            int IntensityMax = 255;
            double Blue;
            double Green;
            double Red;
            double Factor;

            if (Wavelength >= WAVE_LENGTH_VIOLET && Wavelength <= 439)
            {
                Red = -(Wavelength - WAVE_LENGTH_BLUE) / (WAVE_LENGTH_BLUE - WAVE_LENGTH_VIOLET);
                Green = 0.0;
                Blue = 1.0;
            }
            else if (Wavelength >= WAVE_LENGTH_BLUE && Wavelength <= 489)
            {
                Red = 0.0;
                Green = (Wavelength - WAVE_LENGTH_BLUE) / (WAVE_LENGTH_BLUE_LIGHT - WAVE_LENGTH_BLUE);
                Blue = 1.0;
            }
            else if (Wavelength >= WAVE_LENGTH_BLUE_LIGHT && Wavelength <= 509)
            {
                Red = 0.0;
                Green = 1.0;
                Blue = -(Wavelength - WAVE_LENGTH_GREEN) / (WAVE_LENGTH_GREEN - WAVE_LENGTH_BLUE_LIGHT);

            }
            else if (Wavelength >= WAVE_LENGTH_GREEN && Wavelength <= 579)
            {
                Red = (Wavelength - WAVE_LENGTH_GREEN) / (WAVE_LENGTH_YELLOW - WAVE_LENGTH_GREEN);
                Green = 1.0;
                Blue = 0.0;
            }
            else if (Wavelength >= WAVE_LENGTH_YELLOW && Wavelength <= 644)
            {
                Red = 1.0;
                Green = -(Wavelength - WAVE_LENGTH_RED_YELLOW) / (WAVE_LENGTH_RED_YELLOW - WAVE_LENGTH_YELLOW);
                Blue = 0.0;
            }
            else if (Wavelength >= WAVE_LENGTH_RED_YELLOW && Wavelength <= WAVE_LENGTH_RED_VIOLET)
            {
                Red = 1.0;
                Green = 0.0;
                Blue = 0.0;
            }
            else
            {
                Red = 0.0;
                Green = 0.0;
                Blue = 0.0;
            }
            if (Wavelength >= WAVE_LENGTH_VIOLET && Wavelength <= 419)
            {
                Factor = 0.3 + 0.7 * (Wavelength - WAVE_LENGTH_VIOLET) / (WAVE_LENGTH_BLUE_VIOLET - WAVE_LENGTH_VIOLET);
            }
            else if (Wavelength >= WAVE_LENGTH_BLUE_VIOLET && Wavelength <= WAVE_LENGTH_RED)
            {
                Factor = 1.0;
            }
            else if (Wavelength >= 701 && Wavelength <= WAVE_LENGTH_RED_VIOLET)
            {
                Factor = 0.3 + 0.7 * (WAVE_LENGTH_RED_VIOLET - Wavelength) / (WAVE_LENGTH_RED_VIOLET - WAVE_LENGTH_RED);
            }
            else
            {
                Factor = 0.0;
            }

            int R = factorAdjust(Red, Factor, IntensityMax, Gamma);
            int G = factorAdjust(Green, Factor, IntensityMax, Gamma);
            int B = factorAdjust(Blue, Factor, IntensityMax, Gamma);

            return Color.FromArgb(R, G, B);
        }

        private static int factorAdjust(double Color, double Factor, int IntensityMax, double Gamma)
        {
            if (Color == 0.0)
            {
                return 0;
            }
            else
            {
                return (int)Math.Round(IntensityMax * Math.Pow(Color * Factor, Gamma));
            }
        }

        private static Bitmap SetImgOpacity(Image imgPic, float imgOpac)
        {
            Bitmap bmpPic = new Bitmap(imgPic.Width, imgPic.Height);
            Graphics gfxPic = Graphics.FromImage(bmpPic);
            ColorMatrix cmxPic = new ColorMatrix();
            cmxPic.Matrix33 = imgOpac;

            ImageAttributes iaPic = new ImageAttributes();
            iaPic.SetColorMatrix(cmxPic, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            gfxPic.DrawImage(imgPic, new System.Drawing.Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, imgPic.Width, imgPic.Height, GraphicsUnit.Pixel, iaPic);
            gfxPic.Dispose();

            return bmpPic;
        }


        public static Image CreateViewHeatMap(List<ViewHeatMapData> viewParts, int clientWidth, int clientHeight, Image bgImg)
        {
            if (bgImg == null)
            {
                bgImg = CreateNoImageBackground(clientWidth, clientHeight);
            }
            log.WriteInformation("-->CreateViewHeatMap: viewParts:{0},clientWidth:{1},clientHeight:{2}",viewParts.Count,clientWidth,clientHeight);
            int[,] heatMap = new int[clientWidth, clientHeight];
            int maxTimeSpan = 0;//The time span is up color of heat map

            foreach (var curPart in viewParts)
            {
                for (int i = curPart.ScrollLeft; i < clientWidth && i < curPart.ScrollLeft + curPart.ScreenWidth; i++)
                {
                    for (int j = curPart.ScrollTop; j < clientHeight && j < curPart.ScrollTop + curPart.ScreenHeight; j++)
                    {
                        heatMap[i, j] += curPart.TimeSpan;
                        if (heatMap[i, j] > maxTimeSpan) maxTimeSpan = heatMap[i, j];
                    }
                }
            }

            Bitmap bmpPic = new Bitmap(clientWidth, clientHeight);
            using (Graphics g = Graphics.FromImage(bmpPic))
            {
                g.FillRectangle(new SolidBrush(Color.Black), 0, 0, bmpPic.Width, bmpPic.Height);
            }
            for (int i = 0; i < clientWidth; i++)
            {
                for (int j = 0; j < clientHeight; j++)
                {
                    if (heatMap[i, j] > 0)
                    {
                        bmpPic.SetPixel(i, j, getColorFromWaveLength(((int)(heatMap[i, j] * WAVE_LENGTH_DIFF / maxTimeSpan)) + WAVE_LENGTH_BLUE));
                    }
                }
            }
            bmpPic = SetImgOpacity((Image)bmpPic, IMAGE_OPACITY);

            using (Graphics g = Graphics.FromImage(bgImg))
            {
                g.DrawImage(bmpPic, 0, 0);
            }

            return bgImg;
        }

        public static Image CreateClickHeatMap(List<ClickHeatMapData> clicks, int clientWidth, int clientHeight, Image bgImg)
        {
            if (bgImg == null)
            {
                bgImg = CreateNoImageBackground(clientWidth, clientHeight);
            }

            log.WriteInformation("CreateClickHeatMap: clicks:{0},clientWidth:{1},clientHeight:{2}", clicks.Count, clientWidth, clientHeight);
            int maxCounter = clicks.Count > 0 ? clicks.Max(curClick => curClick.Count) : 0;

            Bitmap bmpPic = new Bitmap(clientWidth, clientHeight);
            using (Graphics g = Graphics.FromImage(bmpPic))
            {
                g.FillRectangle(new SolidBrush(Color.Black), 0, 0, bmpPic.Width, bmpPic.Height);
            }
            bmpPic = SetImgOpacity((Image)bmpPic, IMAGE_OPACITY);
            foreach (var curClick in clicks)
            {
                var pntBmp = CreateBlurPoint(33, getColorFromWaveLength(((int)(curClick.Count * WAVE_LENGTH_DIFF / maxCounter)) + WAVE_LENGTH_BLUE));
                //bmpPic.SetPixel(curClick.ClientX, curClick.ClientY, getColorFromWaveLength(((int)(curClick.Count * WAVE_LENGTH_DIFF / maxCounter)) + WAVE_LENGTH_BLUE));
                using (Graphics g = Graphics.FromImage(bmpPic))
                {
                    g.DrawImage(pntBmp, curClick.ClientX-3, curClick.ClientY-3);
                }
            }


            using (Graphics g = Graphics.FromImage(bgImg))
            {
                g.DrawImage(bmpPic, 0, 0);
            }

            return bgImg;
        }

        private static Image CreateBlurPoint(int width, Color color)
        {
            Bitmap bmpPic = new Bitmap(width, width);
            float opacity = 0.03F;

            for (int i = 1; i < width; i++)
            {
                using (Bitmap tmpBm = new Bitmap(width, width))
                {
                    using (Graphics tmpG = Graphics.FromImage(tmpBm))
                    {
                        using (Brush b = new SolidBrush(color))
                        {
                            tmpG.FillEllipse(b, (width-i)/2,(width-i)/2, i, i);
                        }
                    }
                    var opacBmp = SetImgOpacity((Image)tmpBm, opacity);
                    using (Graphics g = Graphics.FromImage(bmpPic))
                    {
                        g.DrawImage(opacBmp, 0, 0);
                    }
                    opacBmp.Dispose();
                }
            }
            return bmpPic;
        }

        private static Image CreateNoImageBackground(int width, int height)
        {
            string nImageStr ="NO IMAGE";
            Bitmap nImgPic = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(nImgPic))
            {
                using (StringFormat s = new StringFormat())
                {
                    s.Alignment = StringAlignment.Center;
                    s.LineAlignment = StringAlignment.Center;
                    SizeF sf;
                    Font nImageFnt = AppropriateFont(g, 12, 1000, new Size(width - 20, height - 20), nImageStr, new Font(FontFamily.GenericSerif, 20, FontStyle.Bold), out sf);
                    g.DrawString(nImageStr, nImageFnt, new SolidBrush(Color.Black), new Rectangle(0, 0, width, height), s);
                }

                //int diagonal = (int)Math.Sqrt(width * width + hight * hight);
                //RotateText(g, new Font(FontFamily.GenericSerif, 20, FontStyle.Bold), "NO IMAGE", -45, new SolidBrush(Color.Black), 100, 100);
            }
            var opacBmp = SetImgOpacity((Image)nImgPic, 0.3F);

            Bitmap bmpPic = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmpPic))
            {
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmpPic.Width, bmpPic.Height);
                g.DrawString(GetGibberish(), new Font(FontFamily.GenericSerif, 12), new SolidBrush(Color.Black), new Rectangle(10, 10, width - 20, height - 20));
                g.DrawImage(opacBmp, 0, 0);
            }
            opacBmp.Dispose();
            return bmpPic;
        }

        public static Font AppropriateFont(Graphics g, float minFontSize, float maxFontSize, Size layoutSize, string s, Font f, out SizeF extent)
        {
            if (maxFontSize == minFontSize)
                f = new Font(f.FontFamily, minFontSize, f.Style);

            extent = g.MeasureString(s, f);

            if (maxFontSize <= minFontSize)
                return f;

            float hRatio = layoutSize.Height / extent.Height;
            float wRatio = layoutSize.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = f.Size * ratio;

            if (newSize < minFontSize)
                newSize = minFontSize;
            else if (newSize > maxFontSize)
                newSize = maxFontSize;

            f = new Font(f.FontFamily, newSize, f.Style);
            extent = g.MeasureString(s, f);

            return f;
        }

        private static void RotateText(Graphics g, Font f, string s, Single angle, Brush b, Single x, Single y)
        {
            if (angle > 360)
            {
                while (angle > 360)
                {
                    angle = angle - 360;
                }
            }
            else if (angle < 0)
            {
                while (angle < 0)
                {
                    angle = angle + 360;
                }
            }

            // Create a matrix and rotate it n degrees.
            Matrix myMatrix = new Matrix();
            myMatrix.Rotate(angle, System.Drawing.Drawing2D.MatrixOrder.Append);

            // Draw the text to the screen after applying the transform.
            g.Transform = myMatrix;
            g.DrawString(s, f, b, x, y);
        }

        private static string GetGibberish()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Lorem ipsum dolor sit amet, eam tota animal ne, mea ex soleat putant. Vim zril instructior ad, iudicabit deterruisset id usu, ne brute erant usu. Eam in eros civibus suscipiantur, ad graeco tractatos duo. Pri cetero gloriatur scriptorem ad, in stet quaestio cum, in essent cetero mel. Vitae albucius disputationi ex usu.");
            sb.AppendLine("Essent maluisset vix id. Stet appellantur eum ne. Mei luptatum disputationi te, vis libris alterum et. Saepe labitur id quo.");
            sb.AppendLine("Liber eligendi mel no. Dicit adipiscing comprehensam vis in, usu veniam exerci ex, eos an accusata oportere. Has scribentur conclusionemque ei, duo posse praesent ex, brute sapientem dissentias vim ex. Dicit dicunt vix no. Viris accommodare sea cu, vis et corpora nominavi voluptatum. Cibo fabulas an quo, his dicant eruditi perfecto eu.");
            sb.AppendLine("Vitae putent reformidans pro an, ut mel iisque delicatissimi. Ius an zril dissentiunt, te mediocrem constituam qui. Vix at clita officiis. Ut qualisque dignissim est. Nam sonet aperiam et.");
            sb.AppendLine("Id cibo noster labores eum. Mea in quem quis case. Sit gubergren instructior in, his ex atqui hendrerit. Eos at libris vulputate cotidieque, eam ne stet abhorreant definiebas. Ad his nonumy constituto, invidunt argumentum mediocritatem ius eu, his eu tota mucius conceptam. Nam an omnes forensibus, in illum mandamus consequuntur duo, vix eu soleat cotidieque. Ius accusam noluisse oporteat ne, ius et agam regione dissentiet.");
            sb.AppendLine("In erant discere nam. Libris albucius atomorum cu nec, et placerat qualisque ius. Dolore nemore similique eam at. Solum ipsum nusquam sed et, sonet veniam vocent te vis. Lorem debitis accommodare his at.");
            sb.AppendLine("Vix ex doming omnesque, ei est impetus euismod delicatissimi, pri consulatu moderatius ex. Eam volumus petentium intellegam eu, te sea everti antiopam suscipiantur. Te consequat constituto consetetur mei, an inciderint accommodare eos, cu eam tota eruditi blandit. At qui legere audiam nonumes. Usu no diam verterem, te accumsan tacimates cum, et his dolor patrioque temporibus.");
            sb.AppendLine("Libris reprehendunt concludaturque eu sit, vix eu partem vocent. Saperet voluptatibus duo ne, bonorum petentium intellegebat mei ex. Pri aliquid vivendum voluptatum in, natum temporibus mei in. Pro cu dictas phaedrum, ne ornatus omittam maluisset has. At eam autem tritani dissentiunt.");
            sb.AppendLine("Ea duo homero cetero, vis viderer aliquam consequat eu. Cu eam elit saperet convenire, eu reque ipsum duo. Vide laoreet instructior pro an, et sit tation diceret reformidans. Pro quem urbanitas at. Nec an elitr appetere efficiendi, his at autem essent principes.");
            sb.AppendLine("Harum ceteros an vis. Soluta corpora adolescens at mei, sea in oratio eruditi molestiae. Ea quod debet graeci nam. Cu cum mundi facilis fastidii. Cu pri enim laboramus.");
            sb.AppendLine("Te vel altera nostrud voluptua. Elit facilis sit te. Autem eripuit at mel, ceteros erroribus contentiones ex cum, mutat summo commune eos et. Malorum repudiare mea eu, an mei vocent malorum hendrerit. Ius sumo percipit convenire an, no diceret evertitur pri. Id sumo adhuc vel, eu eam nisl facilisi.");
            sb.AppendLine("Tamquam quaeque sententiae an eum. Vocibus corrumpit te vis, ex velit solet qui, mel te liber virtute vituperatoribus. An usu vocent iracundia, dico splendide ut nam. Diam corpora facilisis est ex. At voluptaria efficiendi ius. Ut mea assum offendit explicari.");
            sb.AppendLine("Populo debitis ad mel. Omnium oportere mediocrem eu eum, ut corrumpit tincidunt vis. Cum ex labores consetetur temporibus, ius odio contentiones ut. Te nec rebum gloriatur, congue complectitur ea has, id per volumus appellantur.");
            sb.AppendLine("Sea ne aeque dolor, ius aperiri saperet detraxit an. At nam stet populo meliore, usu ea dolorem moderatius quaerendum, in illud consectetuer mei. Eu cum maiorum ancillae conceptam, vix delenit periculis vituperatoribus at, senserit erroribus per at. Ea cum diam etiam verterem. Pro delicata repudiandae ei, case appareat cu mei, sit no alia scribentur. Scripta alterum intellegebat eam id, nam viris utroque contentiones in, elitr iuvaret delectus mea et.");
            sb.AppendLine("An pro vidisse meliore. An quod oportere pertinacia vel, cu modo error vel, at mel apeirian perfecto complectitur. At duo invidunt vivendum, usu an omittam deserunt, ius mollis liberavisse concludaturque ut. Te ridens conceptam contentiones pro, eu nostrum atomorum pericula vix. Putant dissentiunt his no. Vidit semper instructior eos ne, usu at intellegat suscipiantur, nam meliore temporibus ut.");
            sb.AppendLine("At vix facilis petentium conclusionemque, at dicam imperdiet eum. An nominati postulant democritum qui, officiis accommodare delicatissimi mei in. Tollit veniam nemore te nec. Pri ex reque percipit inimicus, usu hinc probo deserunt cu. Nam at malis facilisis, sonet contentiones necessitatibus has ea. An corrumpit adversarium mea, repudiandae consectetuer nec in.");
            sb.AppendLine("Sea ad assum aperiri indoctum, vero commodo ponderum mea ea. At rebum scripserit vis, ut vel impedit salutandi reformidans. Qui ei vide ridens integre, an tritani discere mandamus vel, vero inermis partiendo nec cu. Nam an vocibus lobortis. Equidem singulis antiopam in cum.");
            sb.AppendLine("Veritus accommodare his ut. Mea labores luptatum evertitur eu, augue sententiae pro at, quas alterum persecuti eum te. Mel unum graeco accumsan ex, vis error accusamus no, ius ea tacimates sadipscing. Sea idque elitr conclusionemque ne, nec id ridens dolorem inciderint. Vim at feugiat pericula. Vix iusto menandri volutpat ne.");
            sb.AppendLine("Ad quo nobis aliquam deserunt, eum utinam tractatos ne. Vim agam ludus consequat at. Mazim docendi vel te, est summo moderatius an. An pri omnium scripta intellegat, fierent voluptaria est ad. Case graeco in pri, an eam modo enim animal.");
            sb.AppendLine("Sit at viderer feugait perpetua. Mei ei perfecto sadipscing philosophia. Veri lobortis duo et. Sea nominavi definitionem an, cu integre commune senserit nec. Eam cu nullam discere, erant conceptam signiferumque cum ne, quodsi viderer in pro. Vivendum antiopam vis ei.");
            sb.AppendLine("Ad nec cibo reformidans. Quas eirmod ad cum, eu esse clita numquam sed, nullam omnium ei cum. Tota veri moderatius te vim, et ius rebum albucius conceptam. Ea per dicunt deserunt, in dolores copiosae eam, vel cu mazim epicuri dissentias.");
            sb.AppendLine("Qui in inani mundi suscipit. Vivendum nominati inciderint id pro. His esse quas appellantur ei, et vel altera appareat. Senserit similique reprehendunt sea no, pro doctus persecuti ea. Iusto populo pericula eu his, vim diceret definitionem delicatissimi ne. In virtute impedit sadipscing nam, te latine inermis vim.");
            sb.AppendLine("Mea an assum volutpat, aliquam phaedrum eum ne. Fastidii probatus per id, at deleniti sententiae definiebas sea. Ea tota officiis facilisi has, vel cu summo inermis. Et usu tantas feugiat sensibus. Utinam hendrerit usu et.");
            sb.AppendLine("An quodsi placerat instructior sea. Nec iisque feugait invenire ad, mea an ipsum quando. In nam veri molestie vituperata, te commune petentium has. Quo sonet vidisse in, per alia docendi corrumpit no. Ea pro convenire maluisset reprehendunt, ad his dico torquatos liberavisse, in enim erant choro est.");
            sb.AppendLine("Ius illum simul phaedrum ut, enim veniam utinam sit te. Ne sea inermis adipisci consectetuer, id mundi sadipscing mei. Cum nobis doming delenit in, graeco phaedrum eum eu, ei his graece postulant instructior. Dicat laudem doctus ea per. Soleat percipit expetenda ius ei, ad erat eligendi accommodare has. Cu minim primis scribentur has, esse menandri ut has. Ex eum invenire urbanitas moderatius, natum summo no mel, ex laboramus maiestatis eum.");
            sb.AppendLine("Cum mollis assentior ea, inani liber definiebas no pro. Ridens eligendi pericula cu cum, mel stet minimum perpetua ex. Diam facer voluptaria eam at, vix cu cibo lorem. Eos causae maluisset cu. Etiam accusam elaboraret duo in. Ne ius prima iracundia. Nusquam pericula vituperata no vix.");
            sb.AppendLine("Te pro maiorum nostrum adipisci, usu at enim sensibus. Vis ne albucius recusabo. Sonet laudem at sea, ne mei aperiri denique urbanitas. His sint commodo prompta et, melius expetendis argumentum eos ex, eirmod commune eam in. Eirmod debitis ei usu. Laudem soluta ut has. Pri in etiam eruditi adipiscing.");
            sb.AppendLine("Ius id prima minim imperdiet, disputando adversarium has ut. Saperet corpora noluisse eos no, ad dicit tamquam deleniti vel. Ex electram petentium concludaturque sea, cum utroque commune maiestatis id. Cibo doming vix ne, nec appetere deseruisse ad.");
            sb.AppendLine("Est no libris denique iudicabit, tollit mandamus in usu. Ne eum mandamus tractatos, ne eam nisl vivendum. His te labitur eligendi, id vix expetenda intellegat maiestatis. Nec probatus sadipscing ne. Summo graeco deterruisset vis ne. Et ipsum epicuri vulputate usu, numquam recteque honestatis pro ea. Atqui voluptua ut mei, ut vis sanctus fabulas.");
            sb.AppendLine("Ad doming adipisci vim. Ad postea theophrastus reprehendunt vix. Posse noluisse ut duo. Cu liber utroque efficiantur mei, cum illud quidam ei. Eam ei modus ignota philosophia, quo illum nostro no.");
            sb.AppendLine("Magna facer conceptam pro at, est id tamquam accumsan facilisi. Explicari torquatos vis ex, saepe lobortis voluptatibus et pri. Per tale eligendi in, debet verear integre at duo. An mea singulis elaboraret interpretaris, vel ei autem luptatum, ex persius vivendo qui. Laudem disputationi mel ea, quo denique iracundia conclusionemque ut, vis ex facer euismod. Summo nemore semper est ea, vim ad vidit viris expetendis, vidit malis tacimates et his. Ad consulatu inciderint pro, quo lorem errem appareat ei.");
            sb.AppendLine("Ad maluisset rationibus eloquentiam sit, putant voluptua epicurei at mei, postea fuisset vis an. Inermis eleifend ut usu, mei eu omnis detracto scribentur. No per vocent animal scribentur, sea an equidem inimicus, omnesque tacimates molestiae per an. Id movet omnes lucilius mel, nec ea perpetua intellegat. Sea saperet delicata intellegam ad, eum ad albucius deterruisset.");
            sb.AppendLine("Has ea nulla audiam graecis. Vidit mutat quaeque an quo. Id mei falli eruditi, ut paulo explicari pri. Duo ex affert sententiae assueverit. Cibo salutandi ei duo.");
            sb.AppendLine("Ut sea natum lucilius deserunt, cu sea aeterno quaeque. Et meis labitur nonumes sit, quo ex mazim dolor audire, no molestiae temporibus eum. Postea sanctus te qui, cum legere partiendo appellantur eu. Bonorum facilisi phaedrum ius te. Ius accusam lobortis interpretaris ex, vim unum quas maiorum te, no utinam detraxit suscipiantur eum.");
            sb.AppendLine("Cu sit dicant consul possim, liber soleat ceteros quo ea. Vim case facer integre in. Ad lorem fierent cum, liber nullam putent et cum. Ad commodo scriptorem philosophia vel. Eu sit nostro constituto adversarium, eam partem scaevola cu. Et usu feugiat iracundia, eam ea tacimates elaboraret.");
            sb.AppendLine("Sed ei volumus voluptua, sea vero tation vidisse id. At summo viris vulputate mei, sea an commune honestatis, sea an hinc quando. Nec populo eruditi ne, duo verear definiebas ei. Dolor laboramus efficiantur eum in, errem equidem vocibus no duo, cu cum simul euripidis. Ei duis delicata voluptaria mei, eu utamur pertinax sea.");
            sb.AppendLine("Sea salutandi inciderint dissentiet cu. Est ei brute integre virtute, vis id natum aliquam partiendo. Mea altera oporteat te, soleat oportere accommodare usu ne, te est bonorum eligendi delectus. Ut delenit platonem liberavisse duo.");
            sb.AppendLine("Ea vim augue dolorem, regione explicari persecuti vim no. Dolore malorum comprehensam id quo, omnes putant tamquam per id. Ut mea reprimique adversarium comprehensam, id diceret saperet mea, at movet debitis vim. Eu ius verear civibus. Eos nostrud tractatos et, solet invidunt ut eum, ad quaeque legendos duo.");
            sb.AppendLine("Has porro veritus consequuntur at, quo te wisi latine docendi. Vis an hinc agam. No feugiat perfecto qui, no iudico explicari torquatos ius, posse movet soluta ad eos. Eruditi molestie expetendis his in, quodsi accusam accommodare cum no. Nisl movet tincidunt sed ut, vix postea apeirian perpetua ea, nam et diam esse contentiones.");
            sb.AppendLine("Ne doctus mentitum lucilius sit, ius autem maiorum disputationi ne. Stet mutat voluptua eam te. Ea vel choro voluptaria, mel sale verear te. Ea pri idque iusto, eum aliquam cotidieque at.");
            sb.AppendLine("Pro esse fuisset percipitur in, no suas dicta tempor nec, debet forensibus ne per. Admodum erroribus eu qui. Et agam elitr luptatum usu. Eligendi posidonium pro ut.");
            sb.AppendLine("Luptatum recusabo neglegentur et nam, viderer invidunt senserit an cum, pri ut primis putant persius. An reque blandit evertitur has, vim vide fuisset philosophia ea, recteque suscipiantur eu sea. Et tibique epicuri incorrupte sea, iudico ornatus consequat ei vim. Voluptua moderatius at sea. An has minim laudem vulputate, putent audiam quo te, duo error tation adipisci ne. Phaedrum philosophia ei sit, quo urbanitas omittantur an, vim fugit quaeque te.");
            sb.AppendLine("No tota referrentur mel. Te vivendum patrioque delicatissimi usu, eum nullam omnium no. An pri omnis repudiandae, te sit invenire erroribus maluisset. No velit qualisque pri.");
            sb.AppendLine("Ad mel sensibus vituperata. Eam eu dicat definiebas necessitatibus. Et eos cibo equidem phaedrum, sea ad legere minimum similique, quot primis argumentum ex ius. Postea accusam an per, sonet instructior usu an. Quo id magna invidunt, per malis accusam principes ne. In nec facilisis erroribus iracundia, vocent audiam eu sit. Choro equidem argumentum mel ad, vim id libris discere.");
            sb.AppendLine("Erat eirmod copiosae et sit, cum ut dico duis adhuc. Ius id dolorem corrumpit vulputate, iusto dicam mentitum ius ex, ullum referrentur suscipiantur nam ei. Ne facilis postulant dissentias mei, mel dicunt inimicus cu. In deserunt volutpat moderatius mei, eu his tale commodo oporteat, cu vel stet nibh inciderint. Eum prompta oportere imperdiet ad, an wisi laoreet eum. Id docendi qualisque sea, nam fugit pertinax qualisque at.");
            sb.AppendLine("Dicam debitis intellegebat vel ad. Vis aperiri propriae ne, epicuri elaboraret qui cu. An sed postea causae, etiam detraxit an cum, ei mel enim sale mazim. Est in saperet mandamus consequuntur. Pri ne dolorem denique vivendum, verear facilis sea ea, consulatu vituperata reprimique te sed. Etiam explicari eu quo, pro illum omnesque disputando ex.");
            sb.AppendLine("An quando integre cotidieque eos. An qui sumo propriae, labore partiendo signiferumque has ad, wisi novum dissentiet nec ei. Ea sea erant impetus noluisse, stet dicat fuisset his in, eam id tibique scriptorem. Eos ex adhuc partiendo.");
            sb.AppendLine("Ex nam illum delenit constituam, no soluta euismod volutpat qui. Alii maiestatis no eum. Timeam convenire sententiae vis ne, omnis gubergren no pri, at porro laboramus inciderint pri. Vim aliquam admodum an, oporteat consetetur definiebas at eos. In doming causae vim, per oblique aliquando no. At mundi essent constituam sit.");
            sb.AppendLine("In atqui labore volumus vel. Vis mutat audiam postulant et, ea blandit apeirian eum. Posse delenit delicatissimi at has, pri cu choro nostrud iuvaret. Ut mea facer voluptua, ei fabellas suavitate sed. An mel mundi adipiscing.");
            sb.AppendLine("Qui cibo posse et, ex vel vide laoreet. Has quem lobortis quaestio in. Cum vidit choro concludaturque ne. Nam dolor antiopam et, ne per homero docendi vivendum, vix ad iusto iuvaret probatus. Eos fugit decore petentium ex, noluisse petentium mel ei.");
            sb.AppendLine("Hinc legimus expetendis ex nec. Vix aeque erroribus definitionem id. Te ius appetere phaedrum patrioque, te mei nobis commodo, an his apeirian scriptorem. Volutpat voluptatibus mea ex.");
            sb.AppendLine("Essent quodsi quaestio eam et. Stet veritus salutatus has te. Graece alterum admodum in duo, brute essent platonem quo in. Eligendi dissentiet cu qui, quo vocibus maiorum maluisset ea, vix in facer alterum.");
            sb.AppendLine("Illud homero urbanitas id cum, omnis invidunt disputando ei cum. Quo summo vocent an, tacimates suscipiantur consequuntur sea at. Unum consulatu scriptorem an vis, ne vim regione voluptatum. Ceteros salutatus sed at, dicta eligendi et mel. Eos alterum eruditi verterem ei, ut posse dicta iudico quo, pri cu iusto alienum noluisse. Similique voluptatibus at vel, deserunt praesent vim an, te mentitum intellegat liberavisse eos.");
            sb.AppendLine("Has id omnes veniam. Vix omnium eruditi inciderint ex, nam delicata dissentiet referrentur an, eum putant maiorum neglegentur id. An elitr melius eos. Epicuri reformidans an eos, iisque qualisque sit at. Per nisl urbanitas ut.");
            sb.AppendLine("Sit eros saepe cu, eros tempor essent sit et. Facete nusquam dissentiet ei duo. Cum soleat altera qualisque ne. Ea dicam equidem similique his, ut dissentiunt contentiones quo, est id ignota atomorum dignissim. Ea est vide lorem nostrud, dolorem assueverit eloquentiam mel ut. Ad duo dicat causae dolorum. Eum ei agam ferri explicari, liber inimicus mel at.");
            sb.AppendLine("Partem oporteat id mel. Error omnium rationibus usu ea. Falli blandit no eos, tota copiosae principes ad his. Vix id veri magna. Ei habeo concludaturque his, rebum habemus urbanitas id vim. Quo voluptua eleifend at, feugiat partiendo scribentur sed ne.");
            sb.AppendLine("Te impedit concludaturque per. Puto causae assueverit sea et, nobis praesent eum ne. Mea unum fierent torquatos in, et tantas intellegat mea. Latine scripserit cu vis, persius mnesarchum at eos, no vide dolorem usu.");
            sb.AppendLine("Harum oportere principes ei usu. Quo commune detraxit erroribus ei, cu usu unum iuvaret abhorreant. Ei has alterum delenit, vim nobis vituperata ei. Justo volumus mediocrem ne pro. No vix dicat lucilius electram, prompta intellegebat signiferumque te vel, has aliquip accusata reformidans et. At impetus equidem qui.");
            sb.AppendLine("Eu volutpat honestatis accommodare duo. Mei euismod molestiae ea, ridens utamur cu sit. An mei numquam meliore, libris definiebas eloquentiam at mei. Id mei partem oblique convenire, mutat velit facilis mea id, sea sumo alia cibo id. Per legere prodesset eu, in eos putent officiis expetenda.");
            sb.AppendLine("Usu id lucilius invenire argumentum, per phaedrum conceptam maiestatis ut, cu mollis volutpat nec. Et duo libris malorum, in eos putant appareat. Ex duo civibus postulant consetetur. Cu summo vitae vidisse sed.");
            sb.AppendLine("Mei novum option nusquam id. Postea reformidans sed an. No mei prima praesent, dicant aeterno ei mel, salutatus gloriatur at vel. Delicata mediocritatem usu at. Mea aperiri invidunt dignissim ne.");
            sb.AppendLine("Ea nam debet nonumy, ei eos magna voluptatum. Per at doctus similique. Vocibus probatus quo ad. Vix appareat percipitur in. Cu sit ferri apeirian, vis at soleat propriae.");
            sb.AppendLine("Duo ex oblique similique intellegam, id sit omittam offendit. Ex tractatos efficiendi mediocritatem mei, simul tollit nec et. Illud utamur epicuri eos an, eum in agam idque percipitur. Vel ea velit exerci comprehensam, at per enim lucilius dignissim, pro cu brute accumsan nominati.");
            sb.AppendLine("Elit saepe iracundia eu nec. No vim duis scripta. Cu cum posse virtute instructior, pri postulant disputationi ex, ea meis salutatus neglegentur cum. Mandamus scriptorem an mea, populo malorum sanctus eum no, ad alii patrioque reprehendunt vis. Usu ne stet sale novum.");
            sb.AppendLine("Ferri utinam forensibus vix ex. Vim virtute consectetuer an, qui ne meis veritus inimicus. Id duo epicurei oporteat assentior. Posse partem nostrud sea te, maluisset dissentiet ea duo.");
            sb.AppendLine("Eam iusto utroque volutpat in, sea at numquam dolores euripidis. Stet singulis ius ea. Nam et omnes sadipscing. Eum soleat scripta ea, nam sonet quodsi id. Eum oblique epicuri antiopam ad, vim id case affert everti.");
            sb.AppendLine("Ad pro suas essent, eos possim quaeque in, vim an agam melius mediocritatem. Cum eu fabellas delicata intellegam, mutat labore probatus cum te. Eum ubique albucius accusamus no, adhuc graecis dissentiunt ius in. Mea tantas antiopam mediocrem ei. Paulo congue cu vim. Eum id clita urbanitas consetetur. Quo etiam invidunt facilisis et, vel ei movet sanctus detraxit.");
            sb.AppendLine("His idque altera imperdiet an. Omnis graecis ea nam, vel graeco integre fabellas ea. Lorem appareat instructior ex vis. Ne altera labores voluptua his, qui id possit omnium senserit. An per adolescens vituperatoribus, cu puto antiopam sea.");
            sb.AppendLine("Eu zril vocent vis, eu mel saperet percipit ullamcorper. Case mollis urbanitas eu qui, corpora adipiscing consequuntur in duo, an posidonium percipitur vel. Usu no option postulant, sit ut disputationi reprehendunt. Pri et malis luptatum cotidieque. Verear vivendum an ius, cum suas probatus ut.");
            sb.AppendLine("Pro viderer gubergren in, has ex vero sanctus scribentur. Nam at dignissim consequuntur, nullam diceret vel te, no quo tale sonet inciderint. Tale quaeque in duo, vel wisi theophrastus ne. Cu dolore iracundia per. In nostrum prodesset eam, ad pro tantas accusam. Dictas saperet postulant at mel.");
            sb.AppendLine("Eu consul abhorreant qui, postea omittam phaedrum id quo. Postea putant tamquam ea cum, vim perpetua molestiae ut, ne vim discere persecuti. Ad utroque contentiones duo. Ad pri mandamus senserit qualisque, wisi etiam malorum no qui. Pri eu duis regione alterum, at antiopam intellegebat usu, esse postulant mnesarchum vim ex.");
            sb.AppendLine("Ea mel movet prompta, veniam oportere omittantur sea ei, ius probo assentior cu. Sed primis regione veritus an, an postea liberavisse his, quem ignota fabulas usu ei. Eum ei sale aeque utamur. At mucius explicari sed, has cibo viderer deleniti ei, mel an error ignota gloriatur.");
            sb.AppendLine("Eum ad essent feugiat. Alii insolens antiopam cum ea. Ei mei dicit postulant omittantur. Vim at vivendum salutandi cotidieque. Ei idque doctus ponderum vel, nec ad libris meliore, scripta phaedrum neglegentur ex eos.");
            sb.AppendLine("Vix atqui labitur comprehensam no, sea in minim molestie. Stet tacimates an usu, vel te noster putant perfecto. Alienum theophrastus no cum, deseruisse definitionem cu eam, an quo quem aliquip discere. Est ceteros maiorum perpetua ea, ad qui falli nulla vitae. Usu quod necessitatibus cu. Justo abhorreant et usu, sea accumsan deterruisset an, simul temporibus eam ut.");
            sb.AppendLine("Tation altera te ius, mei ne deleniti perfecto. Nonumy voluptatum pro ad, melius laoreet detracto ea duo. Quo ex suas diceret minimum. Mazim minimum ne sea, debitis denique ad eos. In vim possit suscipit consetetur, duo suas tollit primis eu.");
            sb.AppendLine("An everti aeterno lucilius mel, ne ius solum vivendum. Nihil adversarium mea id, posse cotidieque vim et. Mel at facilisi gloriatur elaboraret, ut has vocent fabellas evertitur. Honestatis conclusionemque mel an. Ad has iusto phaedrum lobortis.");
            sb.AppendLine("Oportere consequuntur ius id, ad veniam aliquando vel, ex soleat accusamus definitiones vim. Eos ne pericula persecuti, vis ne dolore intellegam eloquentiam, ad sit ubique deleniti ullamcorper. Sonet sanctus voluptatibus ei nec, ad vix nisl neglegentur. Suas debitis ea eos, ipsum percipit appellantur ius in. Et sea rationibus reprehendunt, per lorem delenit ne, qui viris solet sententiae te. Sea adhuc graece at.");
            sb.AppendLine("His deserunt voluptaria efficiantur ne, pri petentium expetenda ei. Percipit laboramus assentior ei cum, usu cu fugit mollis. Suas natum et vel. Mutat recteque et pri, usu et amet nobis adolescens. Tation utinam legimus has ut, nam te civibus quaestio.");
            sb.AppendLine("Ex eam timeam commodo tacimates. Alia postea an per. Eum cu graece placerat. Id nam mutat noster intellegebat, tollit elaboraret pri an. Cum homero similique ei, ne dolorum deseruisse nam.");
            sb.AppendLine("Mei cu epicurei intellegat, brute noster te vis, id eam sanctus appareat. Ea elit magna omittantur sea. Et mea magna aperiam denique. Ius ex dico verear, inani neglegentur reprehendunt id sed, eros possim prompta mei ne. Nam no similique mnesarchum appellantur.");
            sb.AppendLine("Ut oratio suscipit concludaturque vix. Vel regione alienum voluptatum ad. Cum omnes pertinax ea, cu qui omnesque necessitatibus. Duo saperet lucilius definitiones at. Oratio fabulas vim eu, appareat gubergren temporibus eu pri, no debitis propriae pri.");
            sb.AppendLine("Blandit luptatum cotidieque ad vim, euismod eruditi volumus ius ei, mazim delenit ad qui. Mundi phaedrum has no. Omnis dicam propriae pri eu. Eos cu placerat efficiendi, vix dico reprehendunt id, malis mnesarchum vel id. No nam qualisque dignissim adolescens, ius in iriure minimum complectitur.");
            sb.AppendLine("Et eum torquatos intellegam. Affert fierent sapientem has at, nec ad scripta aliquam evertitur. Vituperata intellegebat ut cum. Aliquam platonem te vim, ex aliquando constituam sed, ex cum mutat utinam. Eu ferri necessitatibus est, qui deserunt vulputate no. Ad agam liber denique his, qui consul officiis id, error veniam ne per.");
            sb.AppendLine("Eos ex quod vero appetere. Autem simul labores an vim. Quo ea iusto mundi facilis, ne possim gloriatur sed. Cu doming utroque qui, his et fierent copiosae, aeterno adolescens at mei. Mel ea veri dicam.");
            sb.AppendLine("Quo scripserit scribentur at. Est oratio civibus no, nec at quod tamquam inimicus. Ex usu meis natum graecis, mea at sale nemore lucilius, adhuc laudem impedit his at. Hinc sale liber duo ne. Ut sea quis esse dicam, id aliquid evertitur eos, fabulas verterem id eum.");
            sb.AppendLine("Cu his causae facilisis rationibus, usu et libris perfecto scribentur. Te est aliquid recusabo, ipsum blandit ex usu. Habeo maiestatis definitionem ut vis, pri cu possim accusam. Reprimique intellegebat ei mei. Vide detracto epicurei pri at, quis posidonium pri eu.");
            sb.AppendLine("Eam ei delenit lobortis, duis platonem at mea. Decore signiferumque at qui, molestie recusabo appellantur pro et, audire atomorum ut vis. Et qui quod dicat. Ne fierent facilisi nam, integre percipit ex eum.");
            sb.AppendLine("Erant impedit at vix, modo oratio et vim. Sint graece apeirian id usu, reque nullam antiopam ea sit, ius tantas lobortis et. Mei eu cibo integre, facete antiopam electram cu his, ex albucius consetetur mel. Mel invidunt verterem id, mea recusabo accommodare ea.");
            sb.AppendLine("Ad vel detraxit quaerendum, agam graeco equidem ne sea. Nec dolore nostrum in, atqui vitae habemus et his, cu facilis constituam duo. Eu suas diam quidam usu. Est cu vidisse vivendum consetetur, ad velit tritani interpretaris pri.");
            sb.AppendLine("Disputationi definitionem ut vim. Dolore mediocritatem at vim. Elit tibique usu ea, te nisl omnis adversarium usu. Harum verear quaeque per eu. Has puto possim principes in, legimus civibus epicurei nec ne. Ea probo torquatos usu, et vidit fabulas epicuri nam.");
            sb.AppendLine("His quaestio definitiones te, in vel suas vero. Dico latine oportere vim id, cum no reque nemore, sea etiam dolores praesent ei. Ius soluta salutandi scribentur ei, sea nihil detracto ei, mel atqui convenire accusamus cu. Audire mnesarchum ut sed, no pro audire copiosae apeirian, eu pri viris populo volumus. Nisl iuvaret in nec, alii error vituperata sed at. Duo prima nostro alienum ea, omnes evertitur sed ex. Nec in elitr aliquid.");
            sb.AppendLine("Congue nominavi appareat cum at, ne patrioque interesset duo. Numquam fuisset ius no. Quod cotidieque eu per, duo at fugit rationibus, mei suscipit molestiae definitiones eu. Nam magna menandri ad. Novum persius tractatos in mea. Ne placerat dissentias scribentur eos.");
            sb.AppendLine("Pro ne probatus repudiandae, mediocrem scriptorem vis no, ex cum quidam delenit. Qui ut diam fabulas sensibus. Possit possim imperdiet vix ad. Ad vidit assentior vim.");
            sb.AppendLine("Quem conclusionemque in mel. Sit an nisl omnis harum, dolorum petentium ut has. Alia adipiscing an mel, tantas essent eam no. In sea iracundia dignissim moderatius, nostrum dissentiet ex sit, ei vel elitr vitae forensibus. Mei cu alii falli accusamus, alii impedit voluptatum in nam, vel dolore scripta labitur ne.");
            sb.AppendLine("Pro inermis theophrastus at, eu vix lorem malorum accommodare, duo ex eius ridens. Has assum persecuti et, affert fierent comprehensam at est. Ne partiendo consectetuer duo, odio offendit sensibus sed cu. Iriure signiferumque in eum. Cu ius stet eripuit, vix brute offendit ad. Id everti perpetua pri.");
            sb.AppendLine("Ius te timeam expetendis. Graece audiam detraxit eu vim, et mel quis luptatum. Mel dicant vocibus periculis et, sit tation mollis an. Graecis splendide ne sea, pro te veri fastidii. Vivendo tincidunt ne has, vocibus patrioque constituto an his, sea at consul percipitur. Vim cu unum petentium elaboraret, an qualisque dissentiunt nam, nam at diceret gubergren.");
            sb.AppendLine("Soluta putent in nec. Te has adhuc voluptatum. Mea tale stet ne, quod ornatus interesset ei cum. Meis lobortis splendide in per, adolescens deseruisse nec ne. No pro tollit repudiandae, te eam commodo persius, usu ei scaevola mandamus argumentum.");
            sb.AppendLine("Velit viderer propriae ut sed, mea nobis graece cu, pro omittam senserit temporibus ea. Diam perfecto imperdiet mel ei, soluta integre id mei, nibh deserunt at mel. Ad duo dolores percipitur, dico omnes partiendo pro ex. In mollis fabulas cum, nec falli omittam delicata te.");
            sb.AppendLine("Vix et erant meliore fuisset, assum corrumpit mediocritatem sea in, dicta tritani suscipiantur has et. Imperdiet molestiae signiferumque in his, nec ut noster mentitum lobortis. Vis no movet errem torquatos, eu modo fastidii sed. Mea ad mundi latine sensibus, omnis homero repudiare at eum. Ea pro propriae menandri, ut vero utroque facilis pro.");
            sb.AppendLine("Ne per minim regione, eu eros honestatis disputando duo. Eu idque libris commodo sea, honestatis instructior sea no. Vis stet accusam ex. Eam at tollit exerci splendide, mea ne phaedrum persequeris. Tale inimicus cu eam, labitur efficiendi nam id. Te pri civibus appetere accommodare, malis utamur sit ei.");
            sb.AppendLine("In per ullum munere dictas, in eam ancillae appetere. No duo soleat platonem. Ut ius omnis epicuri, has tale inani definitiones id. Et cum suscipit urbanitas, sed te brute appareat necessitatibus. Autem moderatius no has.");
            sb.AppendLine("Nam at minim nullam urbanitas, vix et amet quot referrentur. Mei ea nulla reformidans, lobortis persequeris nam ex. In tota salutandi quaerendum sea. Nec ex ludus accusata concludaturque, illud vivendum in usu. No quo minim persecuti, cu tantas feugiat sit. Duo erat omnes iudicabit ex. Nec prima dolor tincidunt ne, quo.");
            return sb.ToString();
        }
    }
}
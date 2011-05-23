using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EyeTracker.DAL;
using System.Drawing.Imaging;

namespace EyeTracker.Model
{
    public class Analitics
    {
            public interface ITestDataRepository
            {
                List<ViewPartData> GetViewPartsData(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to);
                List<ClickData> GetClicksData(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to);
            }
            public class TestDataRepository : ITestDataRepository
            {
                private Random rnd = new Random();
                private int GetRandomInt(int min, int max)
                {
                    return rnd.Next(min, max);
                }

                public List<ViewPartData> GetViewPartsData(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to)
                {
                    var data = new List<ViewPartData>();
                    for (int i = 0; i < 50; i++)
                    {
                        data.Add(new ViewPartData()
                        {
                            ScrollLeft = 0,
                            ScrollTop = GetRandomInt(0, clientHeight),
                            TimeSpan = GetRandomInt(5, 20)
                        });
                    }
                    return data;
                }

                public List<ClickData> GetClicksData(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to)
                {
                    List<int[]> points = new List<int[]> { 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) } 
                };

                    var data = new List<ClickData>();
                    for (int i = 0; i < 50000; i++)
                    {
                        var curPnt = points[GetRandomInt(0, points.Count)];
                        data.Add(new ClickData()
                        {
                            ClientX = GetRandomInt(curPnt[0] - 20, curPnt[0] + 20),
                            ClientY = GetRandomInt(curPnt[1] - 20, curPnt[1] + 20)
                        });
                        data[i].Count = GetRandomInt(0, (20 - Math.Abs(curPnt[0] - data[i].ClientX)) * 5) + GetRandomInt(0, (20 - Math.Abs(curPnt[1] - data[i].ClientY)) * 5);
                    }
                    return data;
                }
            }

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

            private ITestDataRepository dataRepository;

            public Test()
                : this(new TestDataRepository())
            {

            }

            public Test(ITestDataRepository rep)
            {
                dataRepository = rep;
            }

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

            public class ViewPartData
            {
                public int TimeSpan { get; set; }
                public int ScrollLeft { get; set; }
                public int ScrollTop { get; set; }
            }
            public class ClickData
            {
                public int Count { get; set; }
                public int ClientX { get; set; }
                public int ClientY { get; set; }
            }

            private Image GetViewHeatMap(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to)
            {
                int[,] heatMap = new int[clientWidth, clientHeight];
                int maxTimeSpan = 0;//The time span is up color of heat map

                List<ViewPartData> viewParts = dataRepository.GetViewPartsData(userAppId, pageUri, screenWidth, screenHeight, clientWidth, clientHeight, from, to);
                foreach (var curPart in viewParts)
                {
                    for (int i = curPart.ScrollLeft; i < clientWidth && i < curPart.ScrollLeft + screenWidth; i++)
                    {
                        for (int j = curPart.ScrollTop; j < clientHeight && i < curPart.ScrollTop + screenHeight; j++)
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
                return bmpPic;
            }

            private Image GetClickHeatMap(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to)
            {
                List<ClickData> clicks = dataRepository.GetClicksData(userAppId, pageUri, screenWidth, screenHeight, clientWidth, clientHeight, from, to);
                int maxCounter = clicks.Max(curClick => curClick.Count);

                Bitmap bmpPic = new Bitmap(clientWidth, clientHeight);
                using (Graphics g = Graphics.FromImage(bmpPic))
                {
                    g.FillRectangle(new SolidBrush(Color.Black), 0, 0, bmpPic.Width, bmpPic.Height);
                }
                foreach (var curClick in clicks)
                {
                    bmpPic.SetPixel(curClick.ClientX, curClick.ClientY, getColorFromWaveLength(((int)(curClick.Count * WAVE_LENGTH_DIFF / maxCounter)) + WAVE_LENGTH_BLUE));
                }
                bmpPic = SetImgOpacity((Image)bmpPic, IMAGE_OPACITY);
                return bmpPic;

            }

            public void CreateViewHeatMap()
            {
                Image bg = Image.FromFile(@"C:\Development\TFS2010Workspace\FortuneWebSolution\FortuneWebReportsWSA\excel\Untitled.png");
                Image imgPic = GetViewHeatMap(0, string.Empty, 320, 480, 320, 1030, DateTime.Now, DateTime.Now);
                using (Graphics g = Graphics.FromImage(bg))
                {
                    g.DrawImage(imgPic, 0, 0);
                }
                bg.Save(@"C:\Development\TFS2010Workspace\FortuneWebSolution\FortuneWebReportsWSA\excel\ViewHeatMap.png", ImageFormat.Png);
            }

            public void CreateClickHeatMap()
            {
                Image bg = Image.FromFile(@"C:\Development\TFS2010Workspace\FortuneWebSolution\FortuneWebReportsWSA\excel\Untitled.png");
                Image imgPic = GetClickHeatMap(0, string.Empty, 320, 480, 320, 1030, DateTime.Now, DateTime.Now);
                using (Graphics g = Graphics.FromImage(bg))
                {
                    g.DrawImage(imgPic, 0, 0);
                }
                bg.Save(@"C:\Development\TFS2010Workspace\FortuneWebSolution\FortuneWebReportsWSA\excel\ClickHeatMap.png", ImageFormat.Png);
            }

    }
}

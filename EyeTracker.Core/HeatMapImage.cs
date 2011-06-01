using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using EyeTracker.Core.Models;
using EyeTracker.DAL.Interfaces;

namespace EyeTracker.Models
{
    public static class HeatMapImage
    {
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
            int[,] heatMap = new int[clientWidth, clientHeight];
            int maxTimeSpan = 0;//The time span is up color of heat map

            foreach (var curPart in viewParts)
            {
                for (int i = curPart.ScrollLeft; i < clientWidth && i < curPart.ScrollLeft + curPart.ScreenWidth; i++)
                {
                    for (int j = curPart.ScrollTop; j < clientHeight && i < curPart.ScrollTop + curPart.ScreenHeight; j++)
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
            int maxCounter = clicks.Count > 0 ? clicks.Max(curClick => curClick.Count) : 0;

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

            using (Graphics g = Graphics.FromImage(bgImg))
            {
                g.DrawImage(bmpPic, 0, 0);
            }

            return bgImg;
        }
    }
}
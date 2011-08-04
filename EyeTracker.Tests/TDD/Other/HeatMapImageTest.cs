using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EyeTracker.Core;
using System.Drawing.Imaging;
using System.Drawing;

namespace EyeTracker.Tests.TDD.Other
{
    [TestClass]
    public class HeatMapImageTest
    {
        [TestMethod]
        public void CreateClicksHeatMap()
        {
            var hmFp = new HeatMapImage(100, "c:\\Tmp\\Palette.bmp");
            var hm = new HeatMapImage();
            var clicksList = new List<IntensityPoint>() { 
                new IntensityPoint(){ Intensity = 1, X = 20, Y = 20 }, 
                new IntensityPoint(){ Intensity = 3, X = 60, Y = 60 }, 
                new IntensityPoint(){ Intensity = 10, X = 50, Y = 50 }, 
                new IntensityPoint(){ Intensity = 30, X = 40, Y = 40 }, 
                new IntensityPoint(){ Intensity = 3, X = 90, Y = 90 }, 
                new IntensityPoint(){ Intensity = 33, X = 120, Y = 120 }, 
                new IntensityPoint(){ Intensity = 20, X = 150, Y = 58 }, 
                new IntensityPoint(){ Intensity = 44, X = 45, Y = 234 }, 
            };
            var bitmap = hm.CreateClicksHeatMap(320, 480, clicksList);
            bitmap.Save("c:\\Tmp\\HeatMap.png", ImageFormat.Png);
            bitmap.Dispose();
            bitmap = hmFp.CreateClicksHeatMap(320, 480, clicksList);
            bitmap.Save("c:\\Tmp\\HeatMapFp.png", ImageFormat.Png);
            bitmap.Dispose();


            clicksList = GenerateIntensityPoint(320, 480);

            bitmap = hm.CreateClicksHeatMap(320, 480, clicksList);
            bitmap.Save("c:\\Tmp\\RandomHeatMap.png", ImageFormat.Png);
            bitmap.Dispose();
            bitmap = hmFp.CreateClicksHeatMap(320, 480, clicksList);
            bitmap.Save("c:\\Tmp\\RandomHeatMapFp.png", ImageFormat.Png);
            bitmap.Dispose();
        }


        class MockHeatMapImage : HeatMapImage
        {
            public Color GetColorFromWaveLengthPublic(byte Alpha, double Wavelength)
            {
                return GetColorFromWaveLength(Alpha, Wavelength);
            }
        }

        [TestMethod]
        public void GetColorFromWaveLength()
        {
            var hm = new MockHeatMapImage();
            using (var bm = new Bitmap(275, 20))
            {
                using (var surface = Graphics.FromImage(bm))
                {
                    surface.Clear(Color.Transparent);

                    for (int x = 0; x <= 255; x++)
                    {
                        Color color = hm.GetColorFromWaveLengthPublic(255, HeatMapImage.WAVE_LENGTH_RED - (x * HeatMapImage.WAVE_LENGTH_DIFF / 255));
                        surface.DrawLine(new Pen(color), x + 10, 0, x + 10, 10);
                        surface.DrawLine(new Pen(Color.FromArgb(x,x,x)), x + 10, 10, x + 10, 20);
                    }
                }
                bm.Save("c:\\Tmp\\Palette.png", ImageFormat.Png);
            }
        }

        [TestMethod]
        public void CreateLineHeatMap()
        {
            var hm = new HeatMapImage(100, "c:\\Tmp\\Palette.bmp");
            var bitmap = hm.CreateClicksHeatMap(320, 480, GenerateIntensityLines(320, 480, 50));
            bitmap.Save("c:\\Tmp\\LineHeatMap.png", ImageFormat.Png);
            bitmap.Dispose();

        }

        public static List<IntensityPoint> GenerateIntensityPoint(int width, int height, int count = 500)
        {
            // Initialize random number generator
            Random rRand = new Random();

            // Loop variables
            int iX;
            int iY;
            byte iIntense;

            var clicksList = new List<IntensityPoint>();
            // Lets loop 500 times and create a random point each iteration
            for (int i = 0; i < 200; i++)
            {
                // Pick random locations and intensity
                iX = rRand.Next(0, width);
                iY = rRand.Next(0, height);
                iIntense = (byte)rRand.Next(0, 600);
                // Add heat point to heat points list
                clicksList.Add(new IntensityPoint() { Intensity = iIntense, X = iX, Y = iY });
            }
            return clicksList;
        }

        public static List<IntensityPoint> GenerateIntensityLines(int width, int height, int count = 500)
        {
            // Initialize random number generator
            Random rRand = new Random();

            // Loop variables
            int iStartX;
            int iEndX;
            int iY;
            byte iIntense;

            var clicksList = new List<IntensityPoint>();
            // Lets loop 500 times and create a random point each iteration
            for (int i = 0; i < 200; i++)
            {
                // Pick random locations and intensity
                iStartX = 0;// rRand.Next(0, width);
                iEndX = width;// iStartX + rRand.Next(0, width - iStartX);
                iY = rRand.Next(0, height);
                iIntense = (byte)rRand.Next(0, 600);
                // Add heat point to heat points list
                clicksList.Add(new IntensityLine() { Intensity = iIntense, X = iStartX, EndX = iEndX, Y = iY });
            }
            return clicksList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EyeTracker.Core
{
    public class HeatMapImage
    {
        public const double WAVE_LENGTH_VIOLET = 350;
        public const double WAVE_LENGTH_BLUE_VIOLET = 420;
        public const double WAVE_LENGTH_BLUE = 440;
        public const double WAVE_LENGTH_BLUE_LIGHT = 490;
        public const double WAVE_LENGTH_GREEN = 510;
        public const double WAVE_LENGTH_YELLOW = 580;
        public const double WAVE_LENGTH_RED_YELLOW = 645;
        public const double WAVE_LENGTH_RED = 700;
        public const double WAVE_LENGTH_RED_VIOLET = 780;

        public const double WAVE_LENGTH_DIFF = WAVE_LENGTH_RED - WAVE_LENGTH_BLUE;

        private const int INTENSITY_MAX = 255;

        string paletteBmpPath = null;

        public const int DEFAULT_ALPHA = 100;
        public const int DEFAULT_BG_ALPHA_PERCENT = 50;
        byte alpha = DEFAULT_ALPHA;

        public HeatMapImage(byte alpha = DEFAULT_ALPHA, string paletteBmpPath = null)
        {
            this.paletteBmpPath = paletteBmpPath;
            this.alpha = alpha;
        }

        public Bitmap CreateClicksHeatMap(Bitmap bg, List<IntensityPoint> points)
        {
            Bitmap bMap = new Bitmap(bg.Width, bg.Height);
            AjustIntensity(points);
            bMap = CreateIntensityMask(bMap, points);
            bMap = Colorize(bMap, alpha, paletteBmpPath);
            using (var g = Graphics.FromImage(bg))
            {
                g.DrawImage(bMap,0,0);
            }
            return bg;
        }

        public Bitmap CreateClicksHeatMap(int width, int height, List<IntensityPoint> points)
        {
            Bitmap bMap = new Bitmap(width, height);
            AjustIntensity(points);
            bMap = CreateIntensityMask(bMap, points);
            bMap = Colorize(bMap, alpha, paletteBmpPath);
            return bMap;
        }

        private void AjustIntensity(List<IntensityPoint> points)
        {
            int maxIntensity = points.Max(curItem => curItem.Intensity);
            foreach (var curPnt in points)
            {
                curPnt.Intensity = (int)(curPnt.Intensity * 120 / maxIntensity);
            }
        }

        protected Color GetColorFromWaveLength(byte Alpha, double Wavelength)
        {
            double Gamma = 1.00;
            //int IntensityMax = 255;
            double Blue;
            double Green;
            double Red;
            double Factor;

            if (Wavelength >= WAVE_LENGTH_VIOLET && Wavelength < WAVE_LENGTH_VIOLET)
            {
                Red = -(Wavelength - WAVE_LENGTH_BLUE) / (WAVE_LENGTH_BLUE - WAVE_LENGTH_VIOLET);
                Green = 0.0;
                Blue = 1.0;
            }
            else if (Wavelength >= WAVE_LENGTH_BLUE && Wavelength < WAVE_LENGTH_BLUE_LIGHT)
            {
                Red = 0.0;
                Green = (Wavelength - WAVE_LENGTH_BLUE) / (WAVE_LENGTH_BLUE_LIGHT - WAVE_LENGTH_BLUE);
                Blue = 1.0;
            }
            else if (Wavelength >= WAVE_LENGTH_BLUE_LIGHT && Wavelength < WAVE_LENGTH_GREEN)
            {
                Red = 0.0;
                Green = 1.0;
                Blue = -(Wavelength - WAVE_LENGTH_GREEN) / (WAVE_LENGTH_GREEN - WAVE_LENGTH_BLUE_LIGHT);

            }
            else if (Wavelength >= WAVE_LENGTH_GREEN && Wavelength < WAVE_LENGTH_YELLOW)
            {
                Red = (Wavelength - WAVE_LENGTH_GREEN) / (WAVE_LENGTH_YELLOW - WAVE_LENGTH_GREEN);
                Green = 1.0;
                Blue = 0.0;
            }
            else if (Wavelength >= WAVE_LENGTH_YELLOW && Wavelength < WAVE_LENGTH_RED_YELLOW)
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

            int R = factorAdjust(Red, Factor, INTENSITY_MAX, Gamma);
            int G = factorAdjust(Green, Factor, INTENSITY_MAX, Gamma);
            int B = factorAdjust(Blue, Factor, INTENSITY_MAX, Gamma);

            return Color.FromArgb(Alpha, R, G, B);
        }

        private int factorAdjust(double Color, double Factor, int IntensityMax, double Gamma)
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

        private Bitmap CreateIntensityMask(Bitmap bSurface, List<IntensityPoint> aHeatPoints)
        {
            // Create new graphics surface from memory bitmap
            Graphics DrawSurface = Graphics.FromImage(bSurface);

            // Set background color to white so that pixels can be correctly colorized
            DrawSurface.Clear(Color.White);

            // Traverse heat point data and draw masks for each heat point
            foreach (IntensityPoint DataPoint in aHeatPoints)
            {
                // Render current heat point on draw surface
                DrawHeatPoint(DrawSurface, DataPoint, 15);
            }

            return bSurface;
        }

        private void DrawHeatPoint(Graphics Canvas, IntensityPoint HeatPoint, int Radius)
        {
            // Create points generic list of points to hold circumference points
            List<Point> CircumferencePointsList = new List<Point>();

            // Create an empty point to predefine the point struct used in the circumference loop
            Point CircumferencePoint;

            // Create an empty array that will be populated with points from the generic list
            Point[] CircumferencePointsArray;

            // Calculate ratio to scale byte intensity range from 0-255 to 0-1
            float fRatio = 1F / Byte.MaxValue;
            // Precalulate half of byte max value
            byte bHalf = Byte.MaxValue / 2;
            // Flip intensity on it's center value from low-high to high-low
            int iIntensity = (byte)(HeatPoint.Intensity - ((HeatPoint.Intensity - bHalf) * 2));
            // Store scaled and flipped intensity value for use with gradient center location
            float fIntensity = iIntensity * fRatio;

            int dist = 0;
            var line = HeatPoint as IntensityLine;
            if (line != null)
            {
                dist = line.EndX - line.StartX;
            }


            // Loop through all angles of a circle
            // Define loop variable as a double to prevent casting in each iteration
            // Iterate through loop on 10 degree deltas, this can change to improve performance
            for (double i = 0; i <= 360; i += 10)
            {
                // Replace last iteration point with new empty point struct
                CircumferencePoint = new Point();

                // Plot new point on the circumference of a circle of the defined radius
                // Using the point coordinates, radius, and angle
                // Calculate the position of this iterations point on the circle
                CircumferencePoint.X = Convert.ToInt32(HeatPoint.X + Radius * Math.Cos(ConvertDegreesToRadians(i))) + ((i < 90 || i > 270) ? dist : 0);
                CircumferencePoint.Y = Convert.ToInt32(HeatPoint.Y + Radius * Math.Sin(ConvertDegreesToRadians(i)));

                // Add newly plotted circumference point to generic point list
                CircumferencePointsList.Add(CircumferencePoint);
            }

            // Populate empty points system array from generic points array list
            // Do this to satisfy the datatype of the PathGradientBrush and FillPolygon methods
            CircumferencePointsArray = CircumferencePointsList.ToArray();

            // Create new PathGradientBrush to create a radial gradient using the circumference points
            PathGradientBrush GradientShaper = new PathGradientBrush(CircumferencePointsArray);

            // Create new color blend to tell the PathGradientBrush what colors to use and where to put them
            ColorBlend GradientSpecifications = new ColorBlend(3);

            // Define positions of gradient colors, use intesity to adjust the middle color to
            // show more mask or less mask
            GradientSpecifications.Positions = new float[3] { 0, fIntensity, 1 };
            // Define gradient colors and their alpha values, adjust alpha of gradient colors to match intensity
            GradientSpecifications.Colors = new Color[3]
            {
                Color.FromArgb(0, Color.White),
                Color.FromArgb(HeatPoint.Intensity, Color.Black),
                Color.FromArgb(HeatPoint.Intensity, Color.Black)
            };

            // Pass off color blend to PathGradientBrush to instruct it how to generate the gradient
            GradientShaper.InterpolationColors = GradientSpecifications;

            // Draw polygon (circle) using our point array and gradient brush
            Canvas.FillPolygon(GradientShaper, CircumferencePointsArray);
        }

        private double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        public Bitmap Colorize(Bitmap mask, byte alpha, string palettePath)
        {
            // Create new bitmap to act as a work surface for the colorization process
            var output = new Bitmap(mask.Width, mask.Height, PixelFormat.Format32bppArgb);

            // Create a graphics object from our memory bitmap so we can draw on it and clear it's drawing surface
            var surface = Graphics.FromImage(output);
            surface.Clear(Color.Transparent);

            // Build an array of color mappings to remap our greyscale mask to full color
            // Accept an alpha byte to specify the transparancy of the output image
            ColorMap[] colors = null;
            if (string.IsNullOrEmpty(palettePath))
            {
                colors = CreatePaletteIndex(alpha);
            }
            else
            {
                colors = CreatePaletteIndex(alpha, palettePath);
            }

            // Create new image attributes class to handle the color remappings
            // Inject our color map array to instruct the image attributes class how to do the colorization
            var remapper = new ImageAttributes();
            remapper.SetRemapTable(colors);

            // Draw our mask onto our memory bitmap work surface using the new color mapping scheme
            surface.DrawImage(mask, new Rectangle(0, 0, mask.Width, mask.Height), 0, 0, mask.Width, mask.Height, GraphicsUnit.Pixel, remapper);

            // Send back newly colorized memory bitmap
            return output;
        }

        private ColorMap[] CreatePaletteIndex(byte Alpha, string palettePath)
        {
            ColorMap[] OutputMap = new ColorMap[256];

            // Change this path to wherever you saved the palette image.
            Bitmap Palette = (Bitmap)Bitmap.FromFile(palettePath);

            // Loop through each pixel and create a new color mapping
            for (int x = 0; x <= 255; x++)
            {
                OutputMap[x] = new ColorMap();
                OutputMap[x].OldColor = Color.FromArgb(x, x, x);
                OutputMap[x].NewColor = Color.FromArgb(Alpha, Palette.GetPixel(x, 0));
            }
            int bgAlpha = Alpha - (DEFAULT_BG_ALPHA_PERCENT * Alpha / 100);
            OutputMap[255].NewColor = Color.FromArgb(bgAlpha, OutputMap[255].NewColor);

            return OutputMap;
        }

        private ColorMap[] CreatePaletteIndex(byte Alpha)
        {
            ColorMap[] OutputMap = new ColorMap[256];
          
            // Loop through each pixel and create a new color mapping
            for (int x = 0; x < 255; x++)
            {
                OutputMap[x] = new ColorMap();
                OutputMap[x].OldColor = Color.FromArgb(x, x, x);
                OutputMap[x].NewColor = GetColorFromWaveLength(Alpha, WAVE_LENGTH_RED - (x * WAVE_LENGTH_DIFF / 254));
            }
            int bgAlpha = Alpha - (DEFAULT_BG_ALPHA_PERCENT * Alpha / 100);
            OutputMap[255] = new ColorMap();
            OutputMap[255].OldColor = Color.FromArgb(255, 255, 255);
            OutputMap[255].NewColor = Color.FromArgb(bgAlpha, 53, 53, 53);

            return OutputMap;
        }

        private Bitmap SetImgOpacity(Bitmap imgPic, float imgOpac)
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
    }
}

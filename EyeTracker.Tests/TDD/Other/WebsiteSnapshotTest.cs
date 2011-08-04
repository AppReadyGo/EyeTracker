using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EyeTracker.Core;
using System.Drawing;
using System.Drawing.Imaging;

namespace EyeTracker.Tests.TDD.Other
{
    [TestClass]
    public class WebsiteSnapshotTest
    {
        [TestMethod]
        public void GenerateWebSiteImage()
        {
            var ws = new WebsiteSnapshot("http://google.com", 320);
            Bitmap bitmap = ws.GenerateWebSiteImage();
            bitmap = new HeatMapImage().CreateClicksHeatMap(bitmap, HeatMapImageTest.GenerateIntensityPoint(bitmap.Width, bitmap.Height, 50));
            bitmap.Save("c:\\Tmp\\GoogleSnapshot.png", ImageFormat.Png);
            bitmap.Dispose();
            ws = new WebsiteSnapshot("http://amazon.com", 320);
            bitmap = ws.GenerateWebSiteImage();
            bitmap = new HeatMapImage().CreateClicksHeatMap(bitmap, HeatMapImageTest.GenerateIntensityPoint(bitmap.Width, bitmap.Height, 50));
            bitmap.Save("c:\\Tmp\\AmazonSnapshot.png", ImageFormat.Png);
            bitmap.Dispose();
            ws = new WebsiteSnapshot("http://mobile.nytimes.com", 320);
            bitmap = ws.GenerateWebSiteImage();
            bitmap = new HeatMapImage().CreateClicksHeatMap(bitmap, HeatMapImageTest.GenerateIntensityPoint(bitmap.Width, bitmap.Height, 200));
            bitmap.Save("c:\\Tmp\\NYTimesSnapshot.png", ImageFormat.Png);
            bitmap.Dispose();
        }
    }
}

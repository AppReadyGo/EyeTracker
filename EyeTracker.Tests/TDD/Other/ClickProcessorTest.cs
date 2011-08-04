using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EyeTracker.Core;

namespace EyeTracker.Tests.TDD.Other
{
    [TestClass]
    public class ClickProcessorTest
    {
        [TestMethod]
        public void Merge()
        {
            var clicksList = new List<IntensityPoint>() { 
                new IntensityPoint(){ Intensity = 1, X = 2, Y = 2 }, 
                new IntensityPoint(){ Intensity = 3, X = 6, Y = 6 }, 
                new IntensityPoint(){ Intensity = 1, X = 5, Y = 5 }, 
                new IntensityPoint(){ Intensity = 3, X = 4, Y = 4 }, 
                new IntensityPoint(){ Intensity = 3, X = 4, Y = 1 }, 
                new IntensityPoint(){ Intensity = 3, X = 5, Y = 1 }, 
                new IntensityPoint(){ Intensity = 3, X = 1, Y = 5 }, 
                new IntensityPoint(){ Intensity = 3, X = 1, Y = 4 }, 
            };
            var resList = clicksList.Merge(4);

        }
    }
}

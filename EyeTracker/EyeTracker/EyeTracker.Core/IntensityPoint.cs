using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Core
{
    public class IntensityPoint
    {
        protected int x = 0;
        public int X { get { return x; } set { x = value; } }

        public int Y { get; set; }

        public int  Intensity  { get; set; }
    }

    public class IntensityLine : IntensityPoint
    {
        public int EndX { get; set; }

        public int StartX { get { return x; } set { x = value; } }
    }

}

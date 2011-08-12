using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.DAL.Interfaces
{
    public class ViewHeatMapData
    {
        public int TimeSpan { get; set; }
        public int ScrollLeft { get; set; }
        public int ScrollTop { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
    }
}

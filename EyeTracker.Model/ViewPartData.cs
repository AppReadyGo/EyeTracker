using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Model
{
    private class ViewPartData
    {
        public int TimeSpan { get; set; }
        public int ScrollLeft { get; set; }
        public int ScrollTop { get; set; }
    }

    private class ClickData
    {
        public int Count { get; set; }
        public int ClientX { get; set; }
        public int ClientY { get; set; }
        public int ScreenX { get; set; }
        public int ScreenY { get; set; }
    }
}

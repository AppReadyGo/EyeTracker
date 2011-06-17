
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.DAL.Models
{
    public class AnalyticsInfo
    {
        public class Size
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }
        public Dictionary<long, string> Applications { get; set; }
        public List<string> UriList { get; set; }
        public List<Size> ClientSizes { get; set; }
    }
}

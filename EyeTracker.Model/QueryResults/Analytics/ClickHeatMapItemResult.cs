using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics
{
    public class ClickHeatMapItemResult
    {
        public int Count { get; set; }

        public int ClientX { get; set; }

        public int ClientY { get; set; }
    }
}

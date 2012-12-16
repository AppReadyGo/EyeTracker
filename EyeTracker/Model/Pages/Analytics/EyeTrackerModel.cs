using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;
using EyeTracker.Common.QueryResults.Analytics;

namespace EyeTracker.Model.Pages.Analytics
{
    public class EyeTrackerModel : FilterModel
    {
        public int PointsOnReport { get; set; }

        public IEnumerable<ScreenResult> Screens { get; set; }

        public string UsageChartData { get; set; }
    }
}
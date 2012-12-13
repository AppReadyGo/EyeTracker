using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;
using EyeTracker.Common.QueryResults.Analytics;

namespace EyeTracker.Model.Pages.Analytics
{
    public class FingerPrintModel : FilterModel
    {
        public int PointsOnReport { get; set; }

        public IEnumerable<ScreenResult> Screens { get; set; }
    }
}
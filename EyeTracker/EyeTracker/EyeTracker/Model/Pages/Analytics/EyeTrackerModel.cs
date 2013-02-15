using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;
using EyeTracker.Common.QueryResults.Analytics;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using System.Web.Mvc;

namespace EyeTracker.Model.Pages.Analytics
{
    public class EyeTrackerModel : FilterModel
    {
        public int PointsOnReport { get; set; }

        public IEnumerable<ScreenResult> Screens { get; set; }

        public string UsageChartData { get; set; }

        public EyeTrackerModel(Controller controller, FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(controller, filter, selectedItem, filterDataResult, isSingleMode)
        {
        }
   }
}
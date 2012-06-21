using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;
using EyeTracker.Common.QueryResults.Analytics;

namespace EyeTracker.Model.Pages.Analytics
{
    public class DashboardModel : FilterModel
    {
        public string UsageChartData { get; set; }

        public int MyProperty { get; set; }

        public ContentOverviewResult[] ContentOverviewData { get; set; }
    }
}
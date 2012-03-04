using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;

namespace EyeTracker.Model.Pages.Analytics
{
    public class DashboardModel
    {
        public string UsageChartData { get; set; }

        public FilterModel FilterModel { get; set; }
    }
}
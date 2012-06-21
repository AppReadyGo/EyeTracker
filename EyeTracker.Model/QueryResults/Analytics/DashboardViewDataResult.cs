using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics.QueryResults
{
    public class DashboardViewDataResult : FilterDataResult
    {
        public Dictionary<DateTime, int> Data { get; set; }

        public ContentOverviewResult[] ContentOverview { get; set; }
    }
}

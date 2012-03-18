using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Queries.Analytics.QueryResults
{
    public class UsageViewDataResult : FilterDataResult
    {
        public Dictionary<DateTime, int> Data { get; set; }
    }
}

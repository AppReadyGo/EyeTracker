using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics.QueryResults
{
    public class ABCompareViewDataResult : FilterDataResult
    {
        public bool SecondHasFilteredClicks { get; set; }

        public bool SecondHasClicks { get; set; }
    }
}

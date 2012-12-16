using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics.QueryResults
{
    public class EyeTrackerViewDataResult : FilterDataResult
    {
        public IEnumerable<ScreenResult> Screens { get; set; }
    }
}

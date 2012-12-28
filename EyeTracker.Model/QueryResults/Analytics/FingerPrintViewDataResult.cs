using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics.QueryResults
{
    public class FingerPrintViewDataResult : FilterDataResult
    {
        public IEnumerable<ScreenResult> Screens { get; set; }

        public Dictionary<DateTime, int> VisitsData { get; set; }

        public Dictionary<DateTime, int> ScrollsData { get; set; }

        public Dictionary<DateTime, int> ClicksData { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics.QueryResults
{
    public class FingerPrintViewDataResult : FilterDataResult
    {
        public string ScreenFileExtention { get; set; }

        public int PointsOnReport { get; set; }

        public IEnumerable<ScreenResult> Screens { get; set; }
    }
}

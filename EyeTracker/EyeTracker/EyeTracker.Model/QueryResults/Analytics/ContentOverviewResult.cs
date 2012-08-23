using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics
{
    public class ContentOverviewResult
    {
        public string Path { get; set; }

        public int Views { get; set; }

        public int? ScreenId { get; set; }

        public int ApplicationId { get; set; }
    }
}

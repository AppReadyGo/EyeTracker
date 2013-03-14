using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EyeTracker.Common.QueryResults.Application
{
    public class ExtendedApplicationResult : ApplicationResult
    {
        public IEnumerable<Size> ScreenSizes { get; set; }

        public IEnumerable<string> Pathes { get; set; }

        public long Visits { get; set; }
    }
}

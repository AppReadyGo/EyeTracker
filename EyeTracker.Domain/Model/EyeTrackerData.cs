using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class EyeTrackerData
    {
        public IEnumerable<ScreenSize> ScreenSizes { get; set; }
        public IEnumerable<string> PageUris { get; set; }
    }
}

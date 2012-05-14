using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics.QueryResults
{
    public class ApplicationResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Screens { get; set; }

        public IEnumerable<string> Pathes { get; set; }

        public long Visits { get; set; }
    }
}

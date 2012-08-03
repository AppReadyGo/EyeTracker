using System.Collections.Generic;
using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Common.QueryResults.Portfolio
{
    public class PortfolioResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public IEnumerable<ApplicationResult> Applications { get; set; }

        public long Visits { get; set; }
    }
}

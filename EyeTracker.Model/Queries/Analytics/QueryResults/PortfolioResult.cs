using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Queries.Analytics.QueryResults
{
    public class PortfolioResult
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<ApplicationResult> Applications { get; set; }
    }
}

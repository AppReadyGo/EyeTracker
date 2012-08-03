using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Portfolio
{
    public class PortfolioDataItemResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public long Visits { get; set; }

        public int ApplicationsCount { get; set; }

        public bool IsActive { get; set; }
    }
}

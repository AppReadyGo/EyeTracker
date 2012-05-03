using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Queries.Analytics.QueryResults;

namespace EyeTracker.Common.Queries.Analytics
{
    public class PortfoliosDataResult : FilterDataResult
    {
        public IEnumerable<PortfolioResult> Portfolios1 { get; set; }
    }
}

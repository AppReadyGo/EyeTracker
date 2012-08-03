using System.Collections.Generic;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;

namespace EyeTracker.Common.QueryResults.Portfolio
{
    public class PortfoliosDataResult : PageingResult
    {
        public IEnumerable<PortfolioDataItemResult> Portfolios { get; set; }
    }
}

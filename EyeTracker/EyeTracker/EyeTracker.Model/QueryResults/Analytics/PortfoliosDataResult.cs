using System.Collections.Generic;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;

namespace EyeTracker.Common.QueryResults.Analytics
{
    public class PortfoliosDataResult : FilterDataResult
    {
        public IEnumerable<PortfolioResult> PortfoliosData { get; set; }
    }
}

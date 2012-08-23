using System.Collections.Generic;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;

namespace EyeTracker.Common.QueryResults.Portfolio
{
    public class PortfoliosDataResult : PageingResult
    {
        public IEnumerable<PortfolioDataItemResult> Portfolios { get; set; }

        public IEnumerable<TopApplicationsItemResult> TopApplications { get; set; }

        public IEnumerable<TopScreensItemResult> TopScreens { get; set; }
    }
}

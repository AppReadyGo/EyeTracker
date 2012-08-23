using System.Collections.Generic;
using System.Drawing;
using EyeTracker.Common.QueryResults.Portfolio;
using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Common.QueryResults.Analytics.QueryResults
{
    public class FilterDataResult
    {
        public IEnumerable<PortfolioResult> Portfolios { get; set; }
        public IEnumerable<ApplicationResult> Applications { get; set; }
        public List<Size> ScreenSizes { get; set; }
        public List<string> Pathes { get; private set; }
        public List<string> Languages { get; private set; }
        public List<string> OperatingSystems { get; private set; }
        public List<string> Countries { get; private set; }
        public List<string> Cities { get; private set; }
        public int? ScreenId { get; set; }
    }
}

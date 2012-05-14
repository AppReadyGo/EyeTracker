using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EyeTracker.Core.QueryResults.Analytics.QueryResults
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
    }
}

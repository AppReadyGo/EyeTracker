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
        public Screen ScreenData { get; set; }

        public class Screen
        {
            public int? Id { get; set; }

            public string FileExtention { get; set; }

            public int ClicksAmount { get; set; }

            public int ScrollsAmount { get; set; }

            public bool HasScrolls { get; set; }

            public bool HasClicks { get; set; }
        }
    }
}

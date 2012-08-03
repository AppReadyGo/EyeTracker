using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Application
{
    public class ScreensDataResult : PageingResult
    {
        public int ApplicationId { get; set; }

        public int PortfolioId { get; set; }

        public string ApplicationDescription { get; set; }

        public string PortfolioDescription { get; set; }

        public IEnumerable<ScreenDataItemResult> Screens { get; set; }
    }
}

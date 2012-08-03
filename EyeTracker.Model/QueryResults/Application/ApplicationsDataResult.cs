using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Application
{
    public class ApplicationsDataResult : PageingResult
    {
        public int PortfolioId { get; set; }

        public string PortfolioDescription { get; set; }

        public IEnumerable<ApplicationDataItemResult> Applications { get; set; }
    }
}

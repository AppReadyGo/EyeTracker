using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EyeTracker.Common.QueryResults.Application
{
    public class ScreenDataResult
    {
        public int ApplicationId { get; set; }

        public string ApplicationDescription { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioDescription { get; set; }

        public IEnumerable<string> Pathes { get; set; }

        public IEnumerable<Size> Sizes { get; set; }
    }
}

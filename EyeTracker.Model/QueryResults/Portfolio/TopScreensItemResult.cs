using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EyeTracker.Common.QueryResults.Portfolio
{
    public class TopScreensItemResult
    {
        public int PortfolioId { get; set; }

        public int ApplicationId { get; set; }

        public string Path { get; set; }

        public Size ScreenSize { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Filter
{
    public class FilterModel
    {
        public int PortfolioId { get; set; }

        public bool ShowDateSelector { get; set; }
        public bool ShowApplicationsSelector { get; set; }
        public bool ShowScreenSizesSelector { get; set; }
        public bool ShowPathesSelector { get; set; }

        public DateModel Date { get; set; }
        public SelectorModel Applications { get; set; }
        public SelectorModel ScreenSizes { get; set; }
    }
}
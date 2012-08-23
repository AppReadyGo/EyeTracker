using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeTracker.Model.Filter
{
    public class FilterModel
    {
        public bool ShowDateSelector { get; set; }
        public bool ShowApplicationsSelector { get; set; }
        public bool ShowScreenSizesSelector { get; set; }
        public bool ShowPathesSelector { get; set; }

        public int SelectedPortfolioId { get; set; }
        public int SelectedApplicationId { get; set; }
        public DateTime SelectedDateFrom { get; set; }
        public DateTime SelectedDateTo { get; set; }
        public string SelectedScreenSize { get; set; }
        public string SelectedPath { get; set; }

        public IEnumerable<SelectListItem> Portfolios { get; set; }
        public IEnumerable<SelectListItem> Applications { get; set; }
        public IEnumerable<SelectListItem> Pathes { get; set; }
        public IEnumerable<SelectListItem> ScreenSizes { get; set; }

        public string FormAction { get; set; }

        public string PlaceHolderHTML { get; set; }

        //Json data
        public string PortfoliosData { get; set; }
        public string ApplicationsData { get; set; }

        public bool NoData { get; set; }

        //Top Panel
        public string PortfolioName { get; set; }
        public string Title { get; set; }
        public bool IsSingleMode { get; set; }
        public string ApplicationName { get; set; }
    }
}
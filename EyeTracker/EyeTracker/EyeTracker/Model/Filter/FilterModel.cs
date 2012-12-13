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

        public string GetUrlPart(string path = null)
        {
            if (this.NoData)
            {
                return null;
            }
            else
            {
                path = string.IsNullOrEmpty(path) ? this.SelectedPath : path;

                var parts = new List<string>() { string.Format("pid={0}", this.SelectedPortfolioId) };
                if (this.SelectedApplicationId != 0) parts.Add(string.Format("aid={0}", this.SelectedApplicationId));
                if (!string.IsNullOrEmpty(this.SelectedScreenSize)) parts.Add(string.Format("ss={0}", this.SelectedScreenSize));

                if (!string.IsNullOrEmpty(path)) parts.Add(string.Format("p={0}", HttpUtility.UrlEncode(path)));

                parts.Add(string.Format("fd={0}", this.SelectedDateFrom.ToString("dd-MMM-yyyy")));
                parts.Add(string.Format("td={0}", this.SelectedDateTo.ToString("dd-MMM-yyyy")));
                return "?" + string.Join("&", parts.ToArray());
            }
        }

        public string GetUrlPart(int portfolioId, int applicationId, string screenSize, string path, DateTime dateFrom, DateTime dateTo)
        {
            var parts = new List<string>() { string.Format("pid={0}", portfolioId) };
            if (applicationId != 0) parts.Add(string.Format("aid={0}", applicationId));
            if (!string.IsNullOrEmpty(screenSize)) parts.Add(string.Format("ss={0}", screenSize));

            if (!string.IsNullOrEmpty(path)) parts.Add(string.Format("p={0}", HttpUtility.UrlEncode(path)));

            parts.Add(string.Format("fd={0}", dateFrom.ToString("dd-MMM-yyyy")));
            parts.Add(string.Format("td={0}", dateTo.ToString("dd-MMM-yyyy")));
            return "?" + string.Join("&", parts.ToArray());
        }
    }
}
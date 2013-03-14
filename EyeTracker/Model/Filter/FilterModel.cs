using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using EyeTracker.Model.Pages.Analytics;
using System.Web.Script.Serialization;
using EyeTracker.Common;

namespace EyeTracker.Model.Filter
{
    public class FilterModel : AnalyticsMasterModel
    {
        public int SelectedApplicationId { get; protected set; }
        public DateTime SelectedDateFrom { get; protected set; }
        public DateTime SelectedDateTo { get; protected set; }
        public string SelectedScreenSize { get; protected set; }
        public string SelectedPath { get; protected set; }

        public IEnumerable<SelectListItem> Pathes { get; protected set; }
        public IEnumerable<SelectListItem> ScreenSizes { get; protected set; }

        public string FormAction { get; protected set; }

        public string PlaceHolderHTML { get; protected set; }

        public bool NoData { get; protected set; }
        public int ClicksAmount { get; protected set; }
        public bool HasScrolls { get; protected set; }
        public int ScrollsAmount { get; protected set; }
        public bool HasClicks { get; protected set; }

        //Top Panel
        public string Title { get; protected set; }
        public bool IsSingleMode { get; protected set; }
        public string ApplicationName { get; protected set; }

        public FilterModel(Controller controller, FilterParametersModel filter, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, FilterDataResult filterDataResult, bool isSingleMode, string placeHolderHTML = null)
            : base(controller, leftMenuSelectedItem)
        {
            this.PlaceHolderHTML = placeHolderHTML;

            this.IsSingleMode = isSingleMode;

            this.SelectedDateFrom = filter.FromDate;
            this.SelectedDateTo = filter.ToDate;

            var js = new JavaScriptSerializer();

            this.FormAction = leftMenuSelectedItem.ToString();

            var sizes = new List<SelectListItem>();
            var pathes = new List<SelectListItem>();


            if (filterDataResult.ScreenData != null)
            {
                this.ClicksAmount = filterDataResult.ScreenData.ClicksAmount;
                this.HasScrolls = filterDataResult.ScreenData.HasScrolls;
                this.ScreenId = filterDataResult.ScreenData.Id;
                this.HasClicks = filterDataResult.ScreenData.HasClicks;
                this.ScrollsAmount = filterDataResult.ScreenData.ScrollsAmount;
            }

            this.NoData = !filterDataResult.Applications.SelectMany(a => a.ScreenSizes).Any();

            var curApplication = filterDataResult.Applications.Single(a => a.Id == filter.ApplicationId);

            this.SelectedApplicationId = curApplication.Id;
            this.ApplicationName = curApplication.Description;
                    
            this.SelectedScreenSize = filter.ScreenSize.HasValue ? filter.ScreenSize.Value.ToFormatedString() : (isSingleMode ? curApplication.ScreenSizes.First().ToFormatedString() : null);
            this.SelectedPath = string.IsNullOrEmpty(filter.Path) ? (isSingleMode ? curApplication.Pathes.First() : null) : filter.Path;

            sizes.AddRange(curApplication.ScreenSizes.Select(s => new SelectListItem { Value = s.ToFormatedString(), Text = s.ToFormatedString(), Selected = s.ToFormatedString() == this.SelectedScreenSize }));
            pathes.AddRange(curApplication.Pathes.Select(p => new SelectListItem { Value = p, Text = p, Selected = p == this.SelectedPath }));

            if (!isSingleMode)
            {
                sizes.Insert(0, new SelectListItem { Value = "", Text = "All Sizes", Selected = this.SelectedScreenSize == null });
                pathes.Insert(0, new SelectListItem { Value = "", Text = "All Pathes", Selected = this.SelectedPath == null });
            }

            this.ScreenSizes = sizes;
            this.Pathes = pathes;

            this.FilterUrlPart = GetUrlPart();
       }

        public string GetUrlPart(string path = null)
        {
            if (this.NoData)
            {
                return null;
            }
            else
            {
                path = string.IsNullOrEmpty(path) ? this.SelectedPath : path;

                var parts = new List<string>() { string.Format("aid={0}", this.SelectedApplicationId) };
                if (!string.IsNullOrEmpty(this.SelectedScreenSize)) parts.Add(string.Format("ss={0}", this.SelectedScreenSize));

                if (!string.IsNullOrEmpty(path)) parts.Add(string.Format("p={0}", HttpUtility.UrlEncode(path)));

                parts.Add(string.Format("fd={0}", this.SelectedDateFrom.ToString("dd-MMM-yyyy")));
                parts.Add(string.Format("td={0}", this.SelectedDateTo.ToString("dd-MMM-yyyy")));
                return "?" + string.Join("&", parts.ToArray());
            }
        }

        public string GetUrlPart(int portfolioId, int applicationId, string screenSize, string path, DateTime dateFrom, DateTime dateTo)
        {
            var parts = new List<string>() { string.Format("aid={0}", applicationId) };
            if (!string.IsNullOrEmpty(screenSize)) parts.Add(string.Format("ss={0}", screenSize));

            if (!string.IsNullOrEmpty(path)) parts.Add(string.Format("p={0}", HttpUtility.UrlEncode(path)));

            parts.Add(string.Format("fd={0}", dateFrom.ToString("dd-MMM-yyyy")));
            parts.Add(string.Format("td={0}", dateTo.ToString("dd-MMM-yyyy")));
            return "?" + string.Join("&", parts.ToArray());
        }

        public int? ScreenId { get; set; }
    }
}
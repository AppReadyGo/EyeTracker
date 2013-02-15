using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;
using EyeTracker.Common.QueryResults.Analytics;
using EyeTracker.Model.Master;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using System.Web.Mvc;

namespace EyeTracker.Model.Pages.Analytics
{
    public class DashboardModel : FilterModel
    {
        public string UsageChartData { get; set; }

        public ContentOverviewModel[] ContentOverviewData { get; set; }

        public DashboardModel(Controller controller, FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(controller, filter, selectedItem, filterDataResult, isSingleMode)
        {
        }
    }

    public class ContentOverviewModel
    {
        public string Path { get; set; }
        public int Views { get; set; }
        public int Index { get; set; }
        public bool IndexIsOdd { get { return this.Index % 2 == 0; } }

        public int ApplicationId { get; set; }
        public int? ScreenId { get; set; }

        public string GetPathUrl(string filterPath)
        {
            int pIndx = filterPath.IndexOf("p=");
            if (pIndx > 0)
            {
                int endIndx = filterPath.IndexOf("&", pIndx);
                if (endIndx == -1)
                {
                    endIndx = filterPath.Length;
                }
                filterPath = filterPath.Replace(filterPath.Substring(pIndx, endIndx - pIndx), string.Empty);
            }
            filterPath += "&p=" + HttpUtility.UrlEncode(this.Path);
            return filterPath;
        }
    }
}
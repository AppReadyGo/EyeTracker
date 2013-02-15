using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using System.Web.Mvc;

namespace EyeTracker.Model.Pages.Analytics
{
    public class UsageModel : FilterModel
    {
        public string UsageChartData { get; set; }

        public UsageModel(Controller controller, FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(controller, filter, selectedItem, filterDataResult, isSingleMode)
        {
        }
    }
}
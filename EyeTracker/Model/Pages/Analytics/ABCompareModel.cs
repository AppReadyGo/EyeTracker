﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;
using System.Web.Mvc;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;

namespace EyeTracker.Model.Pages.Analytics
{
    public class ABCompareModel : FilterModel
    {
        public IEnumerable<SelectListItem> FirstScreenPathes { get; set; }

        public IEnumerable<SelectListItem> SecondScreenPathes { get; set; }

        public string FirstPath { get; set; }

        public string SecondPath { get; set; }

        public string FirstScreenUrlPart
        {
            get
            {
                return this.GetUrlPart(this.FirstPath);
            }
        }

        public string SecondScreenUrlPart
        {
            get
            {
                return this.GetUrlPart(this.SecondPath);
            }
        }

        public bool FirstHasFilteredClicks { get; set; }
        public bool FirstHasClicks { get; set; }

        public bool SecondHasFilteredClicks { get; set; }
        public bool SecondHasClicks { get; set; }

        public ABCompareModel(Controller controller, FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(controller, filter, selectedItem, filterDataResult, isSingleMode)
        {
        }
    }
}
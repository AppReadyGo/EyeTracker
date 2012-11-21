using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;
using System.Web.Mvc;

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
    }
}
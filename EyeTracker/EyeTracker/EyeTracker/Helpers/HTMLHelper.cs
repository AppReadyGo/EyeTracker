using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace EyeTracker.Helpers
{
    public static class HTMLHelper
    {
        public static IHtmlString Content(this HtmlHelper helper, string contentPath)
        {
            string path = UrlHelper.GenerateContentUrl(contentPath, helper.ViewContext.HttpContext);
            return helper.Raw(string.Format("{0}?v={1}", path, ConfigurationManager.AppSettings["ContentVersion"]));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using EyeTracker.Model.Master;

namespace EyeTracker.Helpers
{
    public static class HTMLExtentions
    {
        public static IHtmlString BreadCrumbNavigation(this HtmlHelper helper, List<KeyValuePair<string, string>> items)
        {
            var sb = new StringBuilder();
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.Key))
                {
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", item.Key, item.Value);
                }
                else
                {
                    sb.AppendFormat("<span>{0}</span>", item.Value);
                }
            }
            return helper.Raw(sb.ToString());
        }

        public static IHtmlString MenuItem(this HtmlHelper helper, string title, string baseUrl, string filterUrlPart, bool isSelected, bool isDisabled)
        {
            string href = string.Format("href=\"{0}{1}\"", baseUrl, filterUrlPart);
            var classes = new List<string>();
            if (isSelected) classes.Add("active");
            if (isDisabled)
            {
                href = string.Empty;
                classes.Add("disabled");
            }

            return helper.Raw(string.Format("<li class=\"{0}\"><span></span><a {1}>{2}</a></li>", string.Join(" ", classes.ToArray()), href, title));
        }
    }
}
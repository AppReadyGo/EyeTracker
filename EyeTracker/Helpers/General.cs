using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace EyeTracker.Helpers
{
    public static class HTMLExtentions
    {
        public static string BreadCrumbNavigation(this HtmlHelper helper, List<KeyValuePair<string,string>> items)
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
            return sb.ToString();
        }
    }
}
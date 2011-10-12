using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace EyeTracker.Helpers
{
    public static class HTMLTable
    {
        public class Cell
        {
            public string Value { get; set; }
            public Func<string, string> Predicate { get; set; }
            public string StyleClass { get; set; }
            public short RowSpan { get; set; }
            public int ColSpan { get; set; }

            public string GetFormatedValue()
            {
                return Predicate == null ? Value : Predicate(Value);
            }

        }

        public static string Table(this HtmlHelper helper, List<List<Cell>> data, Cell caption = null, List<Cell> columnHeaders = null, List<Cell> rowHeaders = null, List<List<Cell>> footers = null, string cssClass = null, string id = null, string optionals = null)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<table{0}{1}{2}>\n", GetAttribute("class", cssClass), string.IsNullOrEmpty(id) ? "" : "id=\"" + id + "\"", string.IsNullOrEmpty(optionals) ? "" : optionals);
            //Add caption
            if (caption != null)
            {
                sb.AppendFormat("<caption{0}>{1}</caption>\n", GetAttribute("class", caption.StyleClass), caption.GetFormatedValue());
            }
            //Add headers
            if (columnHeaders != null)
            {
                sb.AppendLine("<thead><tr>");
                foreach (var curHeader in columnHeaders)
                {
                    sb.AppendFormat("<th{0}>{1}</th>\n", GetAttribute("class", curHeader.StyleClass), curHeader.GetFormatedValue());
                }
                sb.AppendLine("</thead></tr>");
            }
            //Rows
            sb.AppendLine("<tbody>");
            for (int i = 0; i < data.Count; i++)
            {
                sb.AppendLine("<tr>");
                if (rowHeaders != null)
                {
                    if (i < rowHeaders.Count)
                    {
                        var curRowHeader = rowHeaders[i];
                        sb.AppendFormat("<td{0}>{1}</td>", GetAttribute("class", curRowHeader.StyleClass), curRowHeader.GetFormatedValue());
                    }
                }
                foreach (var curCell in data[i])
                {
                    sb.AppendFormat("<td{0}{2}{3}>{1}</td>", GetAttribute("class", curCell.StyleClass),
                        curCell.GetFormatedValue(),
                        GetAttribute("colspan", curCell.ColSpan),
                        GetAttribute("rowspan", curCell.RowSpan));
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            //Footers
            if (footers != null)
            {
                sb.AppendLine("<tfoot>");
                for (int i = 0; i < footers.Count; i++)
                {
                    sb.AppendLine("<tr>");
                    if (rowHeaders != null)
                    {
                        int rowIndex = (i + data.Count);
                        if (rowIndex < rowHeaders.Count)
                        {
                            var curRowHeader = rowHeaders[rowIndex];
                            sb.AppendFormat("<td{0}>{1}</td>", GetAttribute("class", curRowHeader.StyleClass), curRowHeader.GetFormatedValue());
                        }
                    }
                    foreach (var curCell in footers[i])
                    {
                        sb.AppendFormat("<td{0}>{1}</td>", GetAttribute("class", curCell.StyleClass), curCell.GetFormatedValue());
                    }
                    sb.AppendLine("</tr>");
                }
                sb.AppendLine("</tfoot>");
            }
            sb.AppendLine("</table>");

            return sb.ToString();
        }

        public static string GetAttribute(string attribute, int value)
        {
            return value > 0 ? string.Format(" {0}=\"{1}\"", attribute, value) : string.Empty;
        }

        public static string GetAttribute(string attribute, string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : string.Format(" {0}=\"{1}\"", attribute, value);
        }

        public static string Navigation(this HtmlHelper helper, string idPrefix, string prevCaption, string curCaption, string nextCaption, string cssClass = null)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<table{0}><tbody><tr>\n", GetAttribute("class", cssClass));
            sb.AppendLine("<td class=\"prev\">");
            sb.AppendFormat("<a id=\"{0}_nav_prev_btn\"><span class=\"left-arrow-icon\"></span><span class=\"text\">{1}</span></a>\n", idPrefix, prevCaption);
            sb.AppendLine("</td><td class=\"cur\">");
            sb.AppendFormat("<strong id=\"{0}_nav_cur_text\">{1}</strong>\n", idPrefix, curCaption);
            sb.AppendLine("</td><td class=\"next\">");
            sb.AppendFormat("<a id=\"{0}_nav_next_btn\"><span class=\"text\">{1}</span><span class=\"right-arrow-icon\"></span></a>\n", idPrefix, nextCaption);
            sb.AppendLine("</td></tr></tbody></table>");
            return sb.ToString();
        }

        public static string Paging(this HtmlHelper helper, string idPrefix, int? curPage, int? pagesCount, int rowsInPage, string cssClass = null)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<div class=\"page-nav {0}\">", GetAttribute("class", cssClass));
            if (curPage.HasValue && pagesCount.HasValue)
            {
                sb.AppendFormat("Show rows: <select id=\"{1}_rows_selector\" page=\"{2}\" prevval=\"{3}\">", GetAttribute("class", cssClass), idPrefix, curPage, rowsInPage);
                sb.AppendFormat("<option{0}>20</option>", rowsInPage == 20 ? " selected" : "");
                sb.AppendFormat("<option{0}>60</option>", rowsInPage == 60 ? " selected" : "");
                sb.AppendFormat("<option{0}>100</option></select>", rowsInPage == 100 ? " selected" : "");
                if (pagesCount.Value != int.MaxValue)
                {
                    sb.AppendFormat("<span>{0} of {1}</span> ", curPage, pagesCount);
                }
                else
                {
                    sb.AppendFormat("<span>{0}</span> ", curPage);
                }
                int prevPage = curPage.Value - 1;
                int nextPage = curPage.Value + 1;
                sb.AppendFormat("<a id=\"{0}_prev_btn\" class=\"prev{2}\" page=\"{1}\"></a><a id=\"{0}_next_btn\" class=\"next{4}\" page=\"{3}\"></a></div>", idPrefix, prevPage, prevPage >= 1 ? "" : " disabled", nextPage, nextPage <= pagesCount ? "" : " disabled");
            }
            return sb.ToString();
        }

        public static string UnsortedList(this HtmlHelper helper, string id, IEnumerable<string> strings)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<ul{0}>", string.IsNullOrEmpty(id) ? "" : " id=\"" + id + "\"");
            foreach (var curStr in strings)
            {
                sb.AppendFormat("<li>{0}</li>", curStr);
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}

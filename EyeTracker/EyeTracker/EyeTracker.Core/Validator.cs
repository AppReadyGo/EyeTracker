using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Collections.Generic;
using EyeTracker.Common.Logger;

namespace EyeTracker.Core
{
    public static class Validator
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(typeof(Validator));

        public static bool IsValidBusinessName(string businessName)
        {
            if (string.IsNullOrEmpty(businessName) || businessName.Length < 2 || businessName.Length > 100)
                return false;
            return true;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || email.Length < 5 || email.Length > 50)
                return false;
            Regex regex = new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
            if (!regex.IsMatch(email))
                return false;
            return true;
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6 || password.Length > 20)
                return false;
            else return true;
        }

        public static bool IsValidURL(string url)
        {
            if (string.IsNullOrEmpty(url) || url.Length < 7 || url.Length > 256)
                return false;
            Regex regex = new Regex(@"http:\/\/[A-Za-z0-9\.-]{3,}\.[A-Za-z]{2,}");
            if (!regex.IsMatch(url))
                return false;
            return true;
        }

        public static bool IsValidHTMLContent(string content)
        {
            return true;
        }

        private static readonly Regex HtmlTagExpression = new Regex(@"(?'tag_start'</?)(?'tag'\w+)((\s+(?'attr'(?'attr_name'\w+)(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+)))?)+\s*|\s*)(?'tag_end'/?>)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex WhiteSpaceBetweenHtmlTagsExpression = new Regex(@">(/w+)<", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex HtmlLineBreakExpression = new Regex(@"<br(/s+)/>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Dictionary<string, List<string>> ValidHtmlTags = new Dictionary<string, List<string>> {
	        { "p", new List<string>() },
	        { "br", new List<string>() }, 
	        { "strong", new List<string>() }, 
	        { "b", new List<string>() }, 
	        { "em", new List<string>() }, 
	        { "i", new List<string>() }, 
	        { "u", new List<string>() }, 
	        { "strike", new List<string>() }, 
	        { "ol", new List<string>() }, 
	        { "ul", new List<string>() }, 
	        { "li", new List<string>() }, 
	        { "a", new List<string> { "href" } }, 
	        { "img", new List<string> { "src", "height", "width", "alt" } },
	        { "q", new List<string> { "cite" } }, 
	        { "cite", new List<string>() }, 
	        { "abbr", new List<string>() }, 
	        { "acronym", new List<string>() }, 
	        { "del", new List<string>() }, 
	        { "span", new List<string>{"style"} }, 
	        { "ins", new List<string>() }
        };

        /// <summary>
        /// Toes the safe HTML.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string ToSafeHtml(this string text)
        {
            return text.RemoveInvalidHtmlTags();
        }

        /// <summary>
        /// Removes the invalid HTML tags.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string RemoveInvalidHtmlTags(this string text)
        {
            return HtmlTagExpression.Replace(text, new MatchEvaluator((Match m) =>
            {
                if (!ValidHtmlTags.ContainsKey(m.Groups["tag"].Value))
                    return String.Empty;

                string generatedTag = String.Empty;

                System.Text.RegularExpressions.Group tagStart = m.Groups["tag_start"];
                System.Text.RegularExpressions.Group tagEnd = m.Groups["tag_end"];
                System.Text.RegularExpressions.Group tag = m.Groups["tag"];
                System.Text.RegularExpressions.Group tagAttributes = m.Groups["attr"];

                generatedTag += (tagStart.Success ? tagStart.Value : "<");
                generatedTag += tag.Value;

                foreach (Capture attr in tagAttributes.Captures)
                {
                    int indexOfEquals = attr.Value.IndexOf('=');

                    // don't proceed any futurer if there is no equal sign or just an equal sign
                    if (indexOfEquals < 1)
                        continue;

                    string attrName = attr.Value.Substring(0, indexOfEquals);

                    // check to see if the attribute name is allowed and write attribute if it is
                    if (ValidHtmlTags[tag.Value].Contains(attrName))
                        generatedTag += " " + attr.Value;
                }

                // add nofollow to all hyperlinks
                if (tagStart.Success && tagStart.Value == "<" && tag.Value.Equals("a", StringComparison.OrdinalIgnoreCase))
                    generatedTag += " rel=\"nofollow\"";

                generatedTag += (tagEnd.Success ? tagEnd.Value : ">");

                return generatedTag;
            }));
        }

        //Method to parse Text into HTML
        public static string IntoHTML(this string text)
        {
            //Create a StringBuilder object from the string input
            //parameter
            StringBuilder sb = new StringBuilder(text);
            //Replace all double white spaces with a single white space
            //and &nbsp;
            sb.Replace("  ", " &nbsp;");
            //Check if HTML tags are not allowed
            //if (!allow)
            {
                //Convert the brackets into HTML equivalents
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                //Convert the double quote
                sb.Replace("\"", "&quot;");
            }
            //Create a StringReader from the processed string of
            //the StringBuilder object
            //StringReader sr = new StringReader(sb.ToString());
            //StringWriter sw = new StringWriter();
            //Loop while next character exists
            //while (sr.Peek() > -1)
            //{
            //    //Read a line from the string and store it to a temp
            //    //variable
            //    string temp = sr.ReadLine();
            //    //write the string with the HTML break tag
            //    //Note here write method writes to a Internal StringBuilder
            //    //object created automatically
            //    sw.Write(temp + "<br>");
            //}
            //Return the final processed text
            return sb.ToString();
        }

        public static bool IsValidAlphaNumeric(string friendlyName)
        {
            Regex regex = new Regex(@"\W");
            if (regex.IsMatch(friendlyName))
                return false;
            return true;
        }
    }
}

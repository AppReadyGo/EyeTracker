using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Content
{
    public class MailResult
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string ThemeUrl { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}

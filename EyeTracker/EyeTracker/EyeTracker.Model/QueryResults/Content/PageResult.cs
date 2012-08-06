using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Entities;

namespace EyeTracker.Common.QueryResults.Content
{
    public class PageResult
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string ThemeUrl { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}

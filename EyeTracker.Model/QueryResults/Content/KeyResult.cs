using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Content
{
    public class KeyResult
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public IEnumerable<ItemResult> Items { get; set; }
    }
}

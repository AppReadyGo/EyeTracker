using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Core.Queries
{
    public class GetKeyContentQuery : IQuery<Dictionary<string,string>>
    {
        public string Key { get; set; }

        public GetKeyContentQuery(string key)
        {
            this.Key = key;
        }
    }
}

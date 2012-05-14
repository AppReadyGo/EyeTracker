using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Core.Queries
{
    public class GetPageContent : IQuery<PageContentResult>
    {
        public string Path { get; set; }

        public GetPageContent(string path)
        {
            this.Path = path;
        }
    }
}

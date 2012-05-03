using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EyeTracker.Model.Pages.Analytics
{
    public class FilterParametersModel
    {
        public int pid { get; set; }

        public int? aid { get; set; }

        public DateTime? fd { get; set; }

        public DateTime? td { get; set; }

        public string ss { get; set; }

        public string p { get; set; }

        public string l { get; set; }

        public string os { get; set; }

        public string c { get; set; }

        public string ct { get; set; }

        public bool Validate()
        {
            if (!this.fd.HasValue)
            {
                this.fd = DateTime.UtcNow.AddDays(-30);
            }

            if (!this.td.HasValue)
            {
                this.td = DateTime.UtcNow;
            }
            return true;
        }
    }
}
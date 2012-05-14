using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Pages.Admin
{
    public class LogsModel
    {
        public string SearchStr { get; set; }

        public int CategoryId { get; set; }

        public string Severity { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string ProcessId { get; set; }

        public string ThreadId { get; set; }
    }
}
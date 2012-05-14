using System;
using EyeTracker.Common.Results;
using EyeTracker.Common.Results.Admin;

namespace EyeTracker.Common.Queries.Admin
{
    public class LogDataQuery : IQuery<LogDataResult>
    {
        public string SearchStr { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? CategoryId { get; set; }
        public string Severity { get; set; }
        public string ProcessId { get; set; }
        public string ThreadId { get; set; }

        public LogDataQuery(string searchStr = null, DateTime? fromDate = null, DateTime? toDate = null, int? categoryId = null, string severity = null, string processId = null, string threadId = null)
        {
            this.SearchStr = searchStr;
            this.FromDate = fromDate;
            this.ToDate = toDate;
            this.CategoryId = categoryId;
            this.Severity = severity;
            this.ProcessId = processId;
            this.ThreadId = threadId;
        }
    }
}

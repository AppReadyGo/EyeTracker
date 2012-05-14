using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Results.Admin
{
    public class LogResult
    {
        public int Id { get; set; }
        public int? EventID { get; set; }
        public int Priority { get; set; }
        public string Severity { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public string MachineName { get; set; }
        public string AppDomainName { get; set; }
        public string ProcessID { get; set; }
        public string ProcessName { get; set; }
        public string ThreadName { get; set; }
        public string Win32ThreadId { get; set; }
        public string Message { get; set; }
        public string FormattedMessage { get; set; }
        public IEnumerable<string> Categories { get; set; }

        public override string ToString()
        {
            return string.Format("{2} | ProcessId:{0}, ThreadId:{1}", this.ProcessID, this.Win32ThreadId, this.FormattedMessage);
        }
    }
}

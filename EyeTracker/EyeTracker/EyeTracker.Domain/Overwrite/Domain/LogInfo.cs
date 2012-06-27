using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EyeTracker.DAL.Domain
{
    public class LogInfo
    {

        #region Constants
        public const string AUDIT_LOG_ID = "LogID";
        public const string AUDIT_EVENT_ID = "EventID";
        public const string AUDIT_PRIORITY = "Priority";
        public const string AUDIT_SEVERITY = "Severity";
        public const string AUDIT_TITLE = "Title";
        public const string AUDIT_TIMESTAMP = "Timestamp";
        public const string AUDIT_MACHINE_NAME = "MachineName";
        public const string AUDIT_APP_DOMAIN_NAME = "AppDomainName";
        public const string AUDIT_PROCESS_ID = "ProcessID";
        public const string AUDIT_PROCESS_NAME = "ProcessName";
        public const string AUDIT_THREAD_NAME = "ThreadName";
        public const string AUDIT_WIN32_THREAD_ID = "Win32ThreadId";
        public const string AUDIT_MESSAGE = "Message";
        public const string AUDIT_FORMATTED_MESSAGE = "FormattedMessage";

        public const string CATEGORY_ID = "CategoryID";
        public const string CATEGORY_NAME = "CategoryName";
        #endregion Constants

        public int LogId { get; set; }
        public int? EventId { get; set; }
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

        public List<string> Categories { get; set; }

        public LogInfo(DataRow row)
        {
            LogId = (int)row[AUDIT_LOG_ID];
            EventId = DBNull.Value != row[AUDIT_EVENT_ID] ? (int?)row[AUDIT_EVENT_ID] : null;
            Priority = (int)row[AUDIT_PRIORITY];
            Severity = (string)row[AUDIT_SEVERITY];
            Title = (string)row[AUDIT_TITLE];
            Timestamp = (DateTime)row[AUDIT_TIMESTAMP];
            MachineName = (string)row[AUDIT_MACHINE_NAME];
            AppDomainName = (string)row[AUDIT_APP_DOMAIN_NAME];
            ProcessID = (string)row[AUDIT_PROCESS_ID];
            ProcessName = (string)row[AUDIT_PROCESS_NAME];
            ThreadName = DBNull.Value != row[AUDIT_THREAD_NAME] ? (string)row[AUDIT_THREAD_NAME] : null;
            Win32ThreadId = DBNull.Value != row[AUDIT_WIN32_THREAD_ID] ? (string)row[AUDIT_WIN32_THREAD_ID] : null;
            Message = DBNull.Value != row[AUDIT_MESSAGE] ? (string)row[AUDIT_MESSAGE] : null;
            FormattedMessage = DBNull.Value != row[AUDIT_FORMATTED_MESSAGE] ? (string)row[AUDIT_FORMATTED_MESSAGE] : null;
        }
    }
}

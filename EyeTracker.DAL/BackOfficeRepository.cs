using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace EyeTracker.DAL
{
    public interface IBackOfficeRepository
    {
        LogCollectionsInfo GetLogCollections();
        List<LogInfo> GetLogs(string searchStr, int? category, string severity, DateTime? fromDate, DateTime? toDate, int? processId, int? threadId);
        void ClearLog();
    }

    public class BackOfficeRepository : IBackOfficeRepository
    {
        private const string SP_AUDIT_GET = "[Logging].[Audit_Get]";
        private const string SP_AUDIT_CLEAR = "[Logging].[LogsClear]";
        private const string SP_GET_LOG_COLLECTIONS = "[Logging].[GetLogCollections]";

        private const string SEARCH_STRING = "SearchStr";
        private const string CATEGORY_ID = "CategoryId";
        private const string SEVERITY = "Severity";
        private const string FROM_TIMESTAMP = "FromTimestamp";
        private const string TO_TIMESTAMP = "ToTimestamp";
        private const string PROCESS_ID = "ProcessId";
        private const string THREAD_ID = "ThreadId";
        
        public LogCollectionsInfo GetLogCollections()
        {
            var result = new LogCollectionsInfo();

            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(SP_GET_LOG_COLLECTIONS))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    result.Categories = new Dictionary<int, string>();
                    result.Categories.Add(-1, "All");
                    while (reader.Read())
                    {
                        result.Categories.Add((int)reader["CategoryId"], (string)reader["CategoryName"]);
                    }
                    reader.NextResult();
                    result.Severities = new List<string>();
                    result.Severities.Add("All");
                    while (reader.Read())
                    {
                        result.Severities.Add((string)reader["Severity"]);
                    }
                }
            }
            return result;
        }

        public List<LogInfo> GetLogs(string searchStr, int? category, string severity, DateTime? fromDate, DateTime? toDate, int? processId, int? threadId)
        {
            var result = new List<LogInfo>();

            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(SP_AUDIT_GET))
            {
                if (!string.IsNullOrEmpty(searchStr))
                    database.AddInParameter(command, SEARCH_STRING, DbType.String, searchStr);
                if (category.HasValue)
                    database.AddInParameter(command, CATEGORY_ID, DbType.Int32, category.Value);
                if (!string.IsNullOrEmpty(severity))
                    database.AddInParameter(command, SEVERITY, DbType.String, severity);
                if (fromDate.HasValue)
                    database.AddInParameter(command, FROM_TIMESTAMP, DbType.DateTime, fromDate.Value);
                if (toDate.HasValue)
                    database.AddInParameter(command, TO_TIMESTAMP, DbType.DateTime, toDate.Value);
                if (processId.HasValue)
                    database.AddInParameter(command, PROCESS_ID, DbType.String, processId.Value.ToString());
                if (threadId.HasValue)
                    database.AddInParameter(command, THREAD_ID, DbType.String, threadId.Value.ToString());
                DataSet ds = database.ExecuteDataSet(command);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    var logInfo = new LogInfo(row);
                    result.Add(logInfo);
                }
            }
            return result;
        }

        public void ClearLog()
        {
            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(SP_AUDIT_CLEAR))
            {
                command.CommandTimeout = 60;
                database.ExecuteNonQuery(command);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.Common;
using EyeTracker.DAL.Domain;
using EyeTracker.Common.Logger;
using System.Reflection;

namespace EyeTracker.Core.Services
{
    public interface IBackOfficeService
    {
        OperationResult<List<LogInfo>> GetLogs(string searchStr, int? category, string severity, DateTime? fromDate, DateTime? toDate, int? processId, int? threadId);
        OperationResult<LogCollectionsInfo> GetLogCollections();
        OperationResult ClearLog();
    }

    public class BackOfficeService : IBackOfficeService
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        private IBackOfficeRepository repository;

        public BackOfficeService()
            : this(new BackOfficeRepository())
        {

        }

        public BackOfficeService(IBackOfficeRepository repository)
        {
            this.repository = repository;
        }

        public OperationResult<List<LogInfo>> GetLogs(string searchStr, int? category, string severity, DateTime? fromDate, DateTime? toDate, int? processId, int? threadId)
        {
            try
            {
                return new OperationResult<List<LogInfo>>(repository.GetLogs(searchStr, category, severity, fromDate, toDate, processId, threadId));
            }
            catch (Exception exp)
            {
                return new OperationResult<List<LogInfo>>(true, exp, "BO Error to get logs");
            }
        }
        
        public OperationResult<LogCollectionsInfo> GetLogCollections()
        {
            try
            {
                return new OperationResult<LogCollectionsInfo>(repository.GetLogCollections());
            }
            catch (Exception exp)
            {
                return new OperationResult<LogCollectionsInfo>(true, exp, "BO Error to Get Log Collections");
            }
        }

        public OperationResult ClearLog()
        {
            try
            {
                repository.ClearLog();
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(true, exp,"BO Error to clear logs");
            }
        }
    }
}

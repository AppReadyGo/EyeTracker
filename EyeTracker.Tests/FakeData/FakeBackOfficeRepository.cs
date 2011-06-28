using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.DAL.Models;

namespace EyeTracker.Tests.FakeData
{
    class FakeBackOfficeRepository : IBackOfficeRepository
    {
        public LogCollectionsInfo GetLogCollections()
        {
            throw new NotImplementedException();
        }

        public List<LogInfo> GetLogs(string searchStr, int? category, string severity, DateTime? fromDate, DateTime? toDate, int? processId, int? threadId)
        {
            throw new NotImplementedException();
        }

        public void ClearLog()
        {
            throw new NotImplementedException();
        }
    }
}

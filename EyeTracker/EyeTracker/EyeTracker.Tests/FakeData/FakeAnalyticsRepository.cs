using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.DAL.Interfaces;
using EyeTracker.DAL.Domain;

namespace EyeTracker.Tests.FakeData
{
    class FakeAnalyticsRepository : IAnalyticsRepository
    {
        public List<ClickHeatMapData> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public List<ViewHeatMapData> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public long AddVisitInfo(VisitInfo visitInfo)
        {
            throw new NotImplementedException();
        }

        public void AddViewPartInfo(ViewPartInfo viewPartInfo)
        {
            throw new NotImplementedException();
        }

        public void AddClickInfo(ClickInfo clickInfo)
        {
            throw new NotImplementedException();
        }

        public AnalyticsInfo GetAnalyticsInfo(string userId, long? appId, string pageUri)
        {
            throw new NotImplementedException();
        }

        public void ClearAnalytics(string userId, long appId, string pageUri, int width, int height)
        {
            throw new NotImplementedException();
        }
    }
}

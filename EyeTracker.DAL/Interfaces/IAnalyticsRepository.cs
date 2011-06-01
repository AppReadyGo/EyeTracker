using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.EntityModels;

namespace EyeTracker.DAL.Interfaces
{
    public interface IAnalyticsRepository
    {
        List<ClickHeatMapData> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        List<ViewHeatMapData> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        long AddVisitInfo(VisitInfo visitInfo);

        void AddViewPartInfo(ViewPartInfo viewPartInfo);

        void AddClickInfo(ClickInfo clickInfo);
    }
}

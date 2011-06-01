using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common;
using EyeTracker.DAL.Interfaces;
using EyeTracker.Core.Models;

namespace EyeTracker.Core
{
    public interface IAnalyticsService
    {
        OperationResult<List<ClickHeatMapData>> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);
        OperationResult<List<ViewHeatMapData>> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        OperationResult<long> AddVisitInfo(VisitInfoViewModel visitInfo);

        OperationResult AddViewPartInfo(long visitInfoId, ViewPartInfoViewModel viewPartInfo);

        OperationResult AddClickInfo(long visitInfoId, ClickInfoViewModel clickInfo);
    }
}

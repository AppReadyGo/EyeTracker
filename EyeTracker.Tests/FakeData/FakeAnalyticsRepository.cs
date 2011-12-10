﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.DAL.Domain;
using EyeTracker.Domain.Model.Events;
using EyeTracker.Domain.Model;
using EyeTracker.Domain.Repository;

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

        public long AddVisitInfo(VisitEvent visitInfo)
        {
            throw new NotImplementedException();
        }

        public void AddViewPartInfo(ViewPartEvent viewPartInfo)
        {
            throw new NotImplementedException();
        }

        public void AddClickInfo(ClickEvent clickInfo)
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

        #region IAnalyticsRepository Members

        IEnumerable<ClickHeatMapData> IAnalyticsRepository.GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ViewHeatMapData> IAnalyticsRepository.GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
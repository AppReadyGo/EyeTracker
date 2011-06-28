using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Interfaces;
using EyeTracker.DAL;
using EyeTracker.Common;
using EyeTracker.Core.Models;
using AutoMapper;
using EyeTracker.DAL.EntityModels;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.DAL.Models;

namespace EyeTracker.Core.Services
{
    public interface IAnalyticsService
    {
        OperationResult<List<ClickHeatMapData>> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);
        OperationResult<List<ViewHeatMapData>> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        OperationResult<long> AddVisitInfo(VisitInfoViewModel visitInfo);

        OperationResult AddViewPartInfo(long visitInfoId, ViewPartInfoViewModel viewPartInfo);

        OperationResult AddClickInfo(long visitInfoId, ClickInfoViewModel clickInfo);

        OperationResult<AnalyticsInfo> GetAnalyticsInfo(string userId, long? appId, string pageUri);

        OperationResult<string> GetClientId(long appId);
        OperationResult<long> GetApplicationId(string clientId);

        OperationResult ClearAnalytics(string userId, long appId, string pageUri, int width, int height);
    }
    
    public class AnalyticsService : IAnalyticsService
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IAnalyticsRepository repository;

        public AnalyticsService()
            :this(new AnalyticsRepository())
        {
        }

        public AnalyticsService(IAnalyticsRepository repository)
        {
            this.repository = repository;
        }

        public OperationResult<List<ClickHeatMapData>> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            OperationResult<List<ClickHeatMapData>> result = null;
            try
            {
                log.WriteInformation("GetClickHeatMapData(appId:{0}, pageUri:{1}, clientWidth:{2}, clientHeight:{3}, fromDate:{4}, toDate:{5})", appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
                if (fromDate >= toDate)
                {
                    result = new OperationResult<List<ClickHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else if (clientWidth <= 0 || clientHeight <= 0)
                {
                    result = new OperationResult<List<ClickHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else if (string.IsNullOrEmpty(pageUri))
                {
                    result = new OperationResult<List<ClickHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else
                {
                    result = new OperationResult<List<ClickHeatMapData>>(repository.GetClickHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate));
                }
            }
            catch (Exception exp)
            {
                result = new OperationResult<List<ClickHeatMapData>>(exp, "GetClickHeatMapData(appId:{0},pageUri:{1},clientWidth:{2},clientHeight:{3})",appId, pageUri, clientWidth, clientHeight);
            }
            return result;
        }

        public OperationResult<List<ViewHeatMapData>> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            OperationResult<List<ViewHeatMapData>> result = null;
            try
            {
                if (fromDate >= toDate)
                {
                    result = new OperationResult<List<ViewHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else if (clientWidth <= 0 || clientHeight <= 0)
                {
                    result = new OperationResult<List<ViewHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else if (string.IsNullOrEmpty(pageUri))
                {
                    result = new OperationResult<List<ViewHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else
                {
                    result = new OperationResult<List<ViewHeatMapData>>(repository.GetViewHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate));
                }
            }
            catch (Exception exp)
            {
                result = new OperationResult<List<ViewHeatMapData>>(exp, "GetViewHeatMapData(appId:{0},pageUri:{1},clientWidth:{2},clientHeight:{3})", appId, pageUri, clientWidth, clientHeight);
            }
            return result;
        }

        public OperationResult<long> AddVisitInfo(VisitInfoViewModel visitInfo)
        {
            OperationResult<long> result = null;
            try
            {
                Mapper.CreateMap<VisitInfoViewModel, VisitInfo>()
                    .ForMember(dest => dest.UserApplicationId, opt => opt.MapFrom(src => long.Parse(src.ClientId)));
                var eVisitInfo = Mapper.Map<VisitInfoViewModel, VisitInfo>(visitInfo);
                result = new OperationResult<long>(repository.AddVisitInfo(eVisitInfo));
            }
            catch (Exception exp)
            {
                result = new OperationResult<long>(exp, "AddVisitInfo");
            }
            return result;
        }

        public OperationResult AddViewPartInfo(long visitInfoId, ViewPartInfoViewModel viewPartInfo)
        {
            OperationResult result = null;
            try
            {
                Mapper.CreateMap<ViewPartInfoViewModel, ViewPartInfo>()
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.StrStartDate)))
                    .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => (int)(DateTime.Parse(src.StrFinishDate) - DateTime.Parse(src.StrStartDate)).TotalSeconds));
                    //.ForMember(dest => dest.VisitInfoId, opt => opt.UseValue<long>(visitInfoId));
                var eViewPartInfo = Mapper.Map<ViewPartInfoViewModel, ViewPartInfo>(viewPartInfo);
                eViewPartInfo.VisitInfoId = visitInfoId;
                log.WriteInformation("AddViewPartInfo:visitInfoId:{0}, eViewPartInfo.VisitInfoId:{1}", visitInfoId, eViewPartInfo.VisitInfoId);
                if (eViewPartInfo.TimeSpan > 0)
                {
                    repository.AddViewPartInfo(eViewPartInfo);
                }
                result = new OperationResult();
            }
            catch (Exception exp)
            {
                result = new OperationResult<long>(exp, "AddViewPartInfo");
            }
            return result;
        }

        public OperationResult AddClickInfo(long visitInfoId, ClickInfoViewModel clickInfo)
        {
            OperationResult result = null;
            try
            {
                Mapper.CreateMap<ClickInfoViewModel, ClickInfo>()
                   .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.StrDate)));
                   //.ForMember(dest => dest.VisitInfoId, opt => opt.UseValue<long>(visitInfoId));
                var eClickInfo = Mapper.Map<ClickInfoViewModel, ClickInfo>(clickInfo);
                eClickInfo.VisitInfoId = visitInfoId;
                repository.AddClickInfo(eClickInfo);
                result = new OperationResult();
            }
            catch (Exception exp)
            {
                result = new OperationResult<long>(exp, "AddClickInfo");
            }
            return result;
        }

        public OperationResult<AnalyticsInfo> GetAnalyticsInfo(string userId, long? appId, string pageUri)
        {
            OperationResult<AnalyticsInfo> result = null;
            try
            {
                return new OperationResult<AnalyticsInfo>(repository.GetAnalyticsInfo(userId, appId, pageUri));
            }
            catch(Exception exp)
            {
                result = new OperationResult<AnalyticsInfo>(exp, "GetAnalyticsInfo");
            }
            return result;
        }

        public OperationResult<string> GetClientId(long appId)
        {
            OperationResult<string> result = null;
            try
            {
                result = new OperationResult<string>(Encryption.EncryptLow(appId.ToString(),Encryption.ENCRYPTION_KEY));
            }
            catch (Exception exp)
            {
                result = new OperationResult<string>(exp, "GetClientId(appId:{0})",appId);
            }
            return result;
        }

        public OperationResult<long> GetApplicationId(string clientId)
        {
            OperationResult<long> result = null;
            try
            {
                result = new OperationResult<long>(long.Parse(Encryption.DecryptLow(clientId, Encryption.ENCRYPTION_KEY)));
            }
            catch (Exception exp)
            {
                result = new OperationResult<long>(exp, "GetApplicationId(clientId:{0})", clientId);
            }
            return result;
        }


        #region IAnalyticsService Members


        public OperationResult ClearAnalytics(string userId, long appId, string pageUri, int width, int height)
        {
            OperationResult result = null;
            try
            {
                repository.ClearAnalytics(userId, appId, pageUri, width, height);
                result = new OperationResult();
            }
            catch (Exception exp)
            {
                result = new OperationResult(exp, "ClearAnalytics(userId:{0})", userId);
            }
            return result;
        }

        #endregion
    }
}

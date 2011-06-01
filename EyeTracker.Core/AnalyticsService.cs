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

namespace EyeTracker.Core
{
    public class AnalyticsService : IAnalyticsService
    {
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
                result = new OperationResult<List<ClickHeatMapData>>(ErrorNumber.General);
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
                result = new OperationResult<List<ViewHeatMapData>>(ErrorNumber.General);
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
                result = new OperationResult<long>(ErrorNumber.General);
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
                    .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => (int)(DateTime.Parse(src.StrFinishDate) - DateTime.Parse(src.StrStartDate)).TotalSeconds))
                    .ForMember(dest => dest.VisitInfoId, opt => opt.UseValue<long>(visitInfoId));
                var eViewPartInfo = Mapper.Map<ViewPartInfoViewModel, ViewPartInfo>(viewPartInfo);
                if (eViewPartInfo.TimeSpan > 0)
                {
                    repository.AddViewPartInfo(eViewPartInfo);
                }
                result = new OperationResult();
            }
            catch (Exception exp)
            {
                result = new OperationResult<long>(ErrorNumber.General);
            }
            return result;
        }

        public OperationResult AddClickInfo(long visitInfoId, ClickInfoViewModel clickInfo)
        {
            OperationResult result = null;
            try
            {
                Mapper.CreateMap<ClickInfoViewModel, ClickInfo>()
                   .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.StrDate)))
                   .ForMember(dest => dest.VisitInfoId, opt => opt.UseValue<long>(visitInfoId));
                var eClickInfo = Mapper.Map<ClickInfoViewModel, ClickInfo>(clickInfo);
                repository.AddClickInfo(eClickInfo);
                result = new OperationResult();
            }
            catch (Exception exp)
            {
                result = new OperationResult<long>(ErrorNumber.General);
            }
            return result;
        }
    }
}

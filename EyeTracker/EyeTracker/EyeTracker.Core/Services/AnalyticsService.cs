using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common;
using AutoMapper;
using EyeTracker.Common.Logger;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Net;
using EyeTracker.Domain.Repositories;
using EyeTracker.Domain.Model;
using EyeTracker.Domain.Common;

namespace EyeTracker.Core.Services
{
    public interface IAnalyticsService
    {
        OperationResult<IEnumerable<ClickHeatMapData>> GetClickHeatMapData(int appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        OperationResult<IEnumerable<ViewHeatMapData>> GetViewHeatMapData(int appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        OperationResult<IEnumerable<PortfolioDetails>> GetAllPortfolios();
    }

    public class AnalyticsService : IAnalyticsService
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IAnalyticsRepository repository;
        private IMembershipService membershipService;

        public AnalyticsService()
            : this(new AnalyticsRepository(), new AccountMembershipService())
        {
        }

        public AnalyticsService(IAnalyticsRepository repository, IMembershipService membershipService)
        {
            this.repository = repository;
            this.membershipService = membershipService;
        }

        public OperationResult<IEnumerable<ClickHeatMapData>> GetClickHeatMapData(int appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            OperationResult<IEnumerable<ClickHeatMapData>> result = null;
            try
            {
                log.WriteInformation("GetClickHeatMapData(appId:{0}, pageUri:{1}, clientWidth:{2}, clientHeight:{3}, fromDate:{4}, toDate:{5})", appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
                if (fromDate >= toDate)
                {
                    result = new OperationResult<IEnumerable<ClickHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else if (clientWidth <= 0 || clientHeight <= 0)
                {
                    result = new OperationResult<IEnumerable<ClickHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else if (string.IsNullOrEmpty(pageUri))
                {
                    result = new OperationResult<IEnumerable<ClickHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else
                {
                    result = new OperationResult<IEnumerable<ClickHeatMapData>>(repository.GetClickHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate));
                }
            }
            catch (Exception exp)
            {
                result = new OperationResult<IEnumerable<ClickHeatMapData>>(exp, "GetClickHeatMapData(appId:{0},pageUri:{1},clientWidth:{2},clientHeight:{3})", appId, pageUri, clientWidth, clientHeight);
            }
            return result;
        }

        public OperationResult<IEnumerable<ViewHeatMapData>> GetViewHeatMapData(int appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            OperationResult<IEnumerable<ViewHeatMapData>> result = null;
            try
            {
                if (fromDate >= toDate)
                {
                    result = new OperationResult<IEnumerable<ViewHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else if (clientWidth <= 0 || clientHeight <= 0)
                {
                    result = new OperationResult<IEnumerable<ViewHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else if (string.IsNullOrEmpty(pageUri))
                {
                    result = new OperationResult<IEnumerable<ViewHeatMapData>>(ErrorNumber.WrongParameter);
                }
                else
                {
                    result = new OperationResult<IEnumerable<ViewHeatMapData>>(repository.GetViewHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate));
                }
            }
            catch (Exception exp)
            {
                result = new OperationResult<IEnumerable<ViewHeatMapData>>(exp, "GetViewHeatMapData(appId:{0},pageUri:{1},clientWidth:{2},clientHeight:{3})", appId, pageUri, clientWidth, clientHeight);
            }
            return result;
        }

        public OperationResult<IEnumerable<PortfolioDetails>> GetAllPortfolios()
        {
            try
            {
                var userId = membershipService.GetCurrentUserId();
                if (!userId.HasValue)
                {
                    return new OperationResult<IEnumerable<PortfolioDetails>>(ErrorNumber.AccessDenied);
                }
                return new OperationResult<IEnumerable<PortfolioDetails>>(repository.GetAllPortfolios(userId.Value));
            }
            catch (Exception exp)
            {
                return new OperationResult<IEnumerable<PortfolioDetails>>(exp);
            }
        }
    }
}

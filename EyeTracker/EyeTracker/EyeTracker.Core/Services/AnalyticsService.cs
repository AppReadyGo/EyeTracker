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

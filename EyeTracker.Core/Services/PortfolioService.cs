using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using EyeTracker.Domain.Repositories;
using EyeTracker.Common;

namespace EyeTracker.Core.Services
{
    public interface IPortfolioService
    {
        OperationResult<Portfolio> Get(int id);

        OperationResult<IList<Portfolio>> GetAll();

        OperationResult<IList<Country>> GetCountries();

        OperationResult<int> AddPortfolio(string description, int timeZone);

        OperationResult Update(int id, string description, int timeZone);

        OperationResult Remove(int id);
    }

    public class PortfolioService : IPortfolioService
    {
        IPortfolioRepository repository;
        IMembershipService membershipService;
        
        public PortfolioService()
            : this(new PortfolioRepository(), new AccountMembershipService())
        {
        }

        public PortfolioService(IPortfolioRepository repository, IMembershipService membershipService)
        {
            this.repository = repository;
            this.membershipService = membershipService;
        }

        public OperationResult<Portfolio> Get(int id)
        {
            try
            {
                var userId = membershipService.GetCurrentUserId();
                if (!userId.HasValue)
                {
                    return new OperationResult<Portfolio>(ErrorNumber.AccessDenied);
                }
                return new OperationResult<Portfolio>(repository.Get(id));
            }
            catch (Exception exp)
            {
                return new OperationResult<Portfolio>(exp);
            }
        }

        public OperationResult<IList<Portfolio>> GetAll()
        {
            try
            {
                var userId = membershipService.GetCurrentUserId();
                if (!userId.HasValue)
                {
                    return new OperationResult<IList<Portfolio>>(ErrorNumber.AccessDenied);
                }
                return new OperationResult<IList<Portfolio>>(repository.GetAll(userId.Value));
            }
            catch (Exception exp)
            {
                return new OperationResult<IList<Portfolio>>(exp);
            }
        }

        public OperationResult<IList<Country>> GetCountries()
        {
            try
            {
                return new OperationResult<IList<Country>>(repository.GetCountries());
            }
            catch (Exception exp)
            {
                return new OperationResult<IList<Country>>(exp);
            }
        }

        public OperationResult<int> AddPortfolio(string description, int timeZone)
        {
            try
            {
                //Check Security
                var userId = membershipService.GetCurrentUserId();
                if (!userId.HasValue)
                {
                    return new OperationResult<int>(ErrorNumber.AccessDenied);
                }
                var id = repository.AddPortfolio(description, timeZone, userId.Value);
                return new OperationResult<int>(id);
            }
            catch (Exception exp)
            {
                return new OperationResult<int>(exp);
            }
        }


        public OperationResult Update(int id, string description, int timeZone)
        {
            try
            {
                //Check Security
                var userId = membershipService.GetCurrentUserId();
                if (!userId.HasValue)
                {
                    return new OperationResult<int>(ErrorNumber.AccessDenied);
                }
                repository.Update(id, description, timeZone);
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }

        public OperationResult Remove(int id)
        {
            try
            {
                repository.Remove(id);
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }
    }
}

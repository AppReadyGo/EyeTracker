using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using EyeTracker.Domain.Repository;
using EyeTracker.Common;

namespace EyeTracker.Core.Services
{
    public interface IPortfolioService
    {
        OperationResult<Portfolio> Get(int id);

        OperationResult<IList<Portfolio>> GetAll();

        OperationResult<IList<Country>> GetCountries();
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
                var userRes = membershipService.GetCurrentUserId();
                if (userRes.HasError)
                {
                    return new OperationResult<Portfolio>(userRes);
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
                var userRes = membershipService.GetCurrentUserId();
                if (userRes.HasError)
                {
                    return new OperationResult<IList<Portfolio>>(userRes);
                }
                Guid userId = userRes.Value;
                return new OperationResult<IList<Portfolio>>(repository.GetAll(userId));
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
    }
}

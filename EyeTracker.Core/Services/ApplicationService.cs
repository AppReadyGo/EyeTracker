using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.DAL.Domains;
using EyeTracker.Common;
using EyeTracker.Domain.Model;

namespace EyeTracker.Core.Services
{
    public interface IApplicationService
    {
        OperationResult<int> Add(Application application);
        OperationResult<Application> Get(int appId);
        OperationResult<IList<Application>> GetAll(int portfolioId);
        OperationResult Remove(int appId);
        OperationResult Update(int appId, string description, ApplicationType type);
    }
    public class ApplicationService : IApplicationService
    {
        IApplicationRepository repository = null;
        public ApplicationService() : this(new ApplicationRepository())
        {
        }

        public ApplicationService(IApplicationRepository repository)
        {
            this.repository = repository;
        }

        public OperationResult<int> Add(Application application)
        {
            //Check Security
            //Check application properties
            try
            {
                return new OperationResult<int>(repository.Add(application));
            }
            catch (Exception exp)
            {
                return new OperationResult<int>(exp);
            }
        }

        public OperationResult<Application> Get(int appId)
        {
            //Check Security
            try
            {
                return new OperationResult<Application>(repository.Get(appId));
            }
            catch (Exception exp)
            {
                return new OperationResult<Application>(exp);
            }
        }

        public OperationResult Remove(int appId)
        {
            //Check Security
            try
            {
                repository.Remove(appId);
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }

        public OperationResult Update(int appId, string description, ApplicationType type)
        {
            //Check Security
            //Check application properties
            try
            {
                repository.Update(appId, description, type);
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }


        public OperationResult<IList<Application>> GetAll(int portfolioId)
        {
            //Check Security
            try
            {
                return new OperationResult<IList<Application>>(repository.GetAll(portfolioId));
            }
            catch (Exception exp)
            {
                return new OperationResult<IList<Application>>(exp);
            }
        }
    }
}

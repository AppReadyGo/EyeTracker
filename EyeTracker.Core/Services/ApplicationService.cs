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
        OperationResult Update(int appId, string description);
        OperationResult<long> AddScreen(Screen screen);
        OperationResult<Screen> GetScreen(int appId, int width, int height);

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

        public OperationResult Update(int appId, string description)
        {
            //Check Security
            //Check application properties
            try
            {
                repository.Update(appId, description);
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

        public OperationResult<long> AddScreen(Screen screen)
        {
            try
            {
                return new OperationResult<long>(repository.AddScreen(screen));
            }
            catch (Exception exp)
            {
                return new OperationResult<long>(exp);
            }
        }

        public OperationResult<Screen> GetScreen(int appId, int width, int height)
        {
            try
            {
                return new OperationResult<Screen>(repository.GetScreen(appId, width, height));
            }
            catch (Exception exp)
            {
                return new OperationResult<Screen>(exp);
            }
        }
    }
}

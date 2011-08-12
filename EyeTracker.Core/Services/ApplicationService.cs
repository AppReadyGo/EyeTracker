using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.DAL.Domains;
using EyeTracker.Common;

namespace EyeTracker.Core.Services
{
    public interface IApplicationService
    {
        OperationResult<int> Add(ApplicationInfo application);
        OperationResult<ApplicationInfo> Get(int appId);
        OperationResult<List<ApplicationInfo>> GetAll();
        OperationResult Remove(int appId);
        OperationResult Update(ApplicationInfo application);
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

        public OperationResult<int> Add(ApplicationInfo application)
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

        public OperationResult<ApplicationInfo> Get(int appId)
        {
            //Check Security
            try
            {
                return new OperationResult<ApplicationInfo>(repository.Get(appId));
            }
            catch (Exception exp)
            {
                return new OperationResult<ApplicationInfo>(exp);
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

        public OperationResult Update(ApplicationInfo application)
        {
            //Check Security
            //Check application properties
            try
            {
                repository.Update(application);
                return new OperationResult();
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }


        public OperationResult<List<ApplicationInfo>> GetAll()
        {
            //Check Security
            try
            {
                return new OperationResult<List<ApplicationInfo>>(repository.GetAll());
            }
            catch (Exception exp)
            {
                return new OperationResult<List<ApplicationInfo>>(exp);
            }
        }
    }
}

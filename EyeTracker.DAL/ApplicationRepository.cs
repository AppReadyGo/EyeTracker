using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Domains;

namespace EyeTracker.DAL
{
    public interface IApplicationRepository
    {
        int Add(ApplicationInfo application);

        ApplicationInfo Get(int appId);

        void Remove(int appId);

        void Update(ApplicationInfo application);

        List<ApplicationInfo> GetAll();
    }

    public class ApplicationRepository : IApplicationRepository
    {
        public int Add(ApplicationInfo application)
        {
            throw new NotImplementedException();
        }

        public ApplicationInfo Get(int appId)
        {
            throw new NotImplementedException();
        }

        public void Remove(int appId)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationInfo application)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationInfo> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

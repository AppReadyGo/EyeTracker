using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;

namespace EyeTracker.Tests.FakeData
{
    class FakeApplicationRepository : IApplicationRepository
    {
        public int Add(DAL.Domains.ApplicationInfo application)
        {
            throw new NotImplementedException();
        }

        public DAL.Domains.ApplicationInfo Get(int appId)
        {
            throw new NotImplementedException();
        }

        public void Remove(int appId)
        {
            throw new NotImplementedException();
        }

        public void Update(DAL.Domains.ApplicationInfo application)
        {
            throw new NotImplementedException();
        }

        public List<DAL.Domains.ApplicationInfo> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

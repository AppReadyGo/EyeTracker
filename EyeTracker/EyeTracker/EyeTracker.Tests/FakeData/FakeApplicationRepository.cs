using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.Domain.Model;

namespace EyeTracker.Tests.FakeData
{
    class FakeApplicationRepository : IApplicationRepository
    {
        public int Add(Application application)
        {
            throw new NotImplementedException();
        }

        public Application Get(int appId)
        {
            throw new NotImplementedException();
        }

        public void Remove(int appId)
        {
            throw new NotImplementedException();
        }

        public void Update(Application application)
        {
            throw new NotImplementedException();
        }

        public List<Application> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

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

        #region IApplicationRepository Members


        public void Update(int appId, string description, ApplicationType type)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IApplicationRepository Members


        public IList<Application> GetAll(int portfolioId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IApplicationRepository Members


        public long AddScreen(Screen screen)
        {
            throw new NotImplementedException();
        }

        public Screen GetScreen(int appId, int width, int height)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IApplicationRepository Members


        public void Update(int appId, string description)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IApplicationRepository Members


        public EyeTrackerData GetEyeTrackerData(int appId, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

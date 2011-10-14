using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using NHibernate;
using Library.Common.Data;

namespace EyeTracker.DAL
{
    public interface IApplicationRepository
    {
        int Add(Application application);

        Application Get(int appId);

        void Remove(int appId);

        void Update(Application application);

        List<Application> GetAll();
    }

    public class ApplicationRepository : IApplicationRepository
    {
        public int Add(Application application)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(application);
                    return application.Id;
                }
            }
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

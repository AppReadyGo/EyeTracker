using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Domain;
using NHibernate.Linq;

namespace EyeTracker.DAL
{
    public interface IApplicationRepository
    {
        int Add(Application application);

        Application Get(int appId);

        void Remove(int appId);

        void Update(int appId, string description, ApplicationType type);

        IList<Application> GetAll(int portfolioId);
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
                    transaction.Commit();
                    return application.Id;
                }
            }
        }

        public Application Get(int appId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Application>(appId);
            }
        }

        public void Remove(int appId)
        {
            throw new NotImplementedException();
        }

        public void Update(int appId, string description, ApplicationType type)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var app = session.Get<Application>(appId);
                    app.Update(description, type);
                    transaction.Commit();
                }
            }
        }

        public IList<Application> GetAll(int portfolioId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<Application>()
                    .Where(a => a.Portfolio.Id == portfolioId).ToList();
            }
        }
    }
}

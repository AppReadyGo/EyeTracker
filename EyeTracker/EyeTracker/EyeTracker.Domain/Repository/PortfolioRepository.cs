using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Repositories
{
    public interface IPortfolioRepository
    {
        Portfolio Get(int id);

        IList<Portfolio> GetAll(Guid userId);

        IList<Country> GetCountries();

        int AddPortfolio(string description, int timeZone, Guid guid);

        void Update(int id, string description, int timeZone);

        void Remove(int id);
    }

    public class PortfolioRepository : IPortfolioRepository
    {
        public Portfolio Get(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Portfolio>(id);
            }
        }

        public IList<Portfolio> GetAll(Guid userId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<Portfolio>()
                    .Where(p => p.User.Id == userId).List();
            }
        }

        public IList<Country> GetCountries()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<Country>().List();
            }
        }

        public int AddPortfolio(string description, int timeZone, Guid guid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var user = session.Get<SystemUser>(guid);
                var portfolio = new Portfolio(description, timeZone, user);
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(portfolio);
                    transaction.Commit();
                    return portfolio.Id;
                }
            }
        }

        public void Update(int id, string description, int timeZone)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var portfolio = session.Get<Portfolio>(id);
                using (ITransaction transaction = session.BeginTransaction())
                {
                    portfolio.Update(description, timeZone);
                    session.Update(portfolio);
                    transaction.Commit();
                }
            }
        }

        public void Remove(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var portfolio = session.Get<Portfolio>(id);
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(portfolio);
                    transaction.Commit();
                }
            }
        }
    }
}

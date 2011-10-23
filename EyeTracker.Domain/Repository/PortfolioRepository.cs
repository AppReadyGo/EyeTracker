using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Repository
{
    public interface IPortfolioRepository
    {
        Portfolio Get(int id);

        IList<Portfolio> GetAll(Guid userId);

        IList<Country> GetCountries();

        int AddPortfolio(string description, int countryId, Guid guid);
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

        #region IPortfolioRepository Members


        public int AddPortfolio(string description, int countryId, Guid guid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var country = session.QueryOver<Country>().Where(c => c.GeoId == countryId).SingleOrDefault();
                var user = session.QueryOver<SystemUser>().Where(u => u.Id == guid).SingleOrDefault();
                var portfolio = new Portfolio(description, country, user);
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(portfolio);
                    transaction.Commit();
                    return portfolio.Id;
                }
            }
        }

        #endregion
    }
}

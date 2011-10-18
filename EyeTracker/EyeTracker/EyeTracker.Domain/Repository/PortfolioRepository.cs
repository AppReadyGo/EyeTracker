using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using NHibernate;
using Library.Common.Data;

namespace EyeTracker.Domain.Repository
{
    public interface IPortfolioRepository
    {
        Portfolio Get(int id);

        IList<Portfolio> GetAll(Guid userId);

        IList<Country> GetCountries();
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
    }
}

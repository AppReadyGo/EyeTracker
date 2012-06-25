using System;
using System.Linq;
using System.Collections.Generic;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Domain.Common;

namespace EyeTracker.Domain.Repositories
{
    public interface IAnalyticsRepository
    {
        IEnumerable<PortfolioDetails> GetAllPortfolios(int userId);
    }
    
    public class AnalyticsRepository : IAnalyticsRepository
    {
        public IEnumerable<PortfolioDetails> GetAllPortfolios(int userId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var portfolios = session.Query<Portfolio>()
                    .Where(p => p.User.Id == userId)
                    .Select(p => new
                    {
                        key = p.Id,
                        details = new PortfolioDetails
                            {
                                Id = p.Id,
                                Description = p.Description,
                                Visits = 0
                            }
                    }).ToList();

                var portfoliosIds = portfolios.Select(p => p.key).ToArray();

                var applications = session.Query<Application>()
                    .Where(a => portfoliosIds.Contains(a.Portfolio.Id))
                    .Select(a => new { key = a.Portfolio.Id, app = new ApplicationDetails
                                {
                                    Id = a.Id,
                                    Description = a.Description,
                                    Visits = 0
                                }});

                foreach(var item in portfolios)
                {
                    item.details.Applications = applications.Where(ai => ai.key == item.key).Select(ai => ai.app).ToList();
                }
                return portfolios.Select(p => p.details);
            }
        }
    }
}

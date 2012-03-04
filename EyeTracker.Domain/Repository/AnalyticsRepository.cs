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
        IEnumerable<ClickHeatMapData> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        IEnumerable<ViewHeatMapData> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        DashboardData GetDashboardData(int portfolioId, int? applicationId, DateTime fromDate, DateTime toDate);

        IEnumerable<PortfolioDetails> GetCurrentUserPortfolios(Guid userId);
    }
    
    public class AnalyticsRepository : IAnalyticsRepository
    {
        public IEnumerable<ClickHeatMapData> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result = session.Query<PageView>()
                    .Where(p => p.Application.Id == appId &&
                                p.Path == pageUri &&
                                p.ClientWidth == clientHeight &&
                                p.ClientHeight == clientHeight &&
                                p.Date >= fromDate &&
                                p.Date <= toDate)
                    .SelectMany(p => p.Clicks)
                    .GroupBy(c => new { X = c.X, Y = c.Y })
                    .Select(c => new ClickHeatMapData { ClientX = c.Key.X, ClientY = c.Key.Y, Count = c.Count() })
                    .ToList();
                return result;
            }
        }

        public IEnumerable<ViewHeatMapData> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result = session.Query<PageView>()
                    .Where(p => p.Application.Id == appId &&
                                p.Path == pageUri &&
                                p.ClientWidth == clientHeight &&
                                p.ClientHeight == clientHeight &&
                                p.Date >= fromDate &&
                                p.Date <= toDate)
                    .Select(p => p)
                    .ToList()
                    .SelectMany(p => p.ViewParts.Select(vp => new { X = vp.X, Y = vp.Y, ScreenWidth = p.ClientWidth, ScreenHeight = p.ScreenHeight, Date = vp.Date }))
                    .GroupBy(c => c)
                    .Select(c => new ViewHeatMapData { ScrollLeft = c.Key.X, ScrollTop = c.Key.Y, ScreenHeight = c.Key.ScreenHeight, ScreenWidth = c.Key.ScreenWidth, TimeSpan = c.Count() })
                    .ToList();
                return result;
            }
        }

        public DashboardData GetDashboardData(int portfolioId, int? applicationId, DateTime fromDate, DateTime toDate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                DashboardData res;
                if (applicationId.HasValue)
                {
                    var dashboard = new ApplicationDashboardData();
                    var application = session.Get<Application>(applicationId.Value);

                    dashboard.Description = application.Description;
                    dashboard.PortfolioId = application.Portfolio.Id;
                    dashboard.PortfolioDescription = application.Portfolio.Description;

                    dashboard.ViewsData = session.Query<PageView>()
                       .Where(pv => pv.Application.Id == applicationId.Value && pv.Date >= fromDate && pv.Date <= toDate)
                       .GroupBy(g => g.Date)
                       .Select(g => new KeyValuePair<DateTime, int>(g.Key, g.Count()))
                       .ToList().ToDictionary(v => v.Key, v => v.Value);
                    res = dashboard;
                }
                else
                {
                    var dashboard = new DashboardData();
                    var portfolio = session.Get<Portfolio>(portfolioId);

                    dashboard.Description = portfolio.Description;

                    IEnumerable<int> appIds = portfolio.Applications.Select(a => a.Id).ToArray();

                    dashboard.ViewsData = session.Query<PageView>()
                        .Where(pv => appIds.Contains(pv.Application.Id) && pv.Date >= fromDate && pv.Date <= toDate)
                        .GroupBy(g => g.Date)
                        .Select(g => new KeyValuePair<DateTime, int>(g.Key, g.Count()))
                        .ToList().ToDictionary(v => v.Key, v => v.Value);
                    res = dashboard;
                }

                return res;
            }
        }

        public IEnumerable<PortfolioDetails> GetCurrentUserPortfolios(Guid userId)
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

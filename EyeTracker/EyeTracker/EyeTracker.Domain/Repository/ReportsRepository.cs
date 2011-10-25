using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using EyeTracker.Domain.Model;
using NHibernate.Linq;

namespace EyeTracker.Domain.Repository
{
    public interface IReportsRepository
    {
        Dictionary<DateTime, int> GetVisitsData(DateTime from, DateTime to, int? portfolioId, int? applicationId, DataGrouping dataGrouping);
    }

    public class ReportsRepository : IReportsRepository
    {
        public Dictionary<DateTime, int> GetVisitsData(DateTime from, DateTime to, int? portfolioId, int? applicationId, DataGrouping dataGrouping)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var query = session.Query<PageView>();
                if(applicationId.HasValue)
                {
                    query = query.Where(pv => pv.Application.Id == applicationId.Value);
                }
                else if (portfolioId.HasValue)
                {
                    query = query.Where(pv => pv.Application.Portfolio.Id == portfolioId.Value);
                }
                var pageViews = query.Where(pv => pv.Date >= from && pv.Date <= to.Date).ToList();
                Dictionary<DateTime, int> result = null;
                switch (dataGrouping)
                {
                    case DataGrouping.Minute:
                        result = pageViews.GroupBy(g => new DateTime(g.Date.Year, g.Date.Month, g.Date.Day, g.Date.Hour, g.Date.Minute, 0)).ToDictionary(k => k.Key, v => v.Count());
                        break;
                    case DataGrouping.Hour:
                        result = pageViews.GroupBy(g => new DateTime(g.Date.Year, g.Date.Month, g.Date.Day, g.Date.Hour, 0, 0)).ToDictionary(k => k.Key, v => v.Count());
                        break;
                    case DataGrouping.Day:
                        result = pageViews.GroupBy(g => g.Date.Date).ToDictionary(k => k.Key, v => v.Count());
                        break;
                    case DataGrouping.Month:
                        result = pageViews.GroupBy(g => new DateTime(g.Date.Year, g.Date.Month, 1)).ToDictionary(k => k.Key, v => v.Count());
                        break;
                    case DataGrouping.Year:
                        result = pageViews.GroupBy(g => new DateTime(g.Date.Year, 1, 1)).ToDictionary(k => k.Key, v => v.Count());
                        break;
                }
                return result;
            }
        }
    }
}

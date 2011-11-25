using System;
using System.Linq;
using System.Collections.Generic;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.Repository
{
    public interface IAnalyticsRepository
    {
        IEnumerable<ClickHeatMapData> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        IEnumerable<ViewHeatMapData> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

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
    }
}

using System.Collections.Generic;
using System.Linq;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Queries.Analytics;
using EyeTracker.Common.QueryResults.Analytics;
using EyeTracker.Common;
using EyeTracker.Domain.Model;

namespace EyeTracker.Domain.Queries
{
    public class ClickHeatMapDataQueryHandler : IQueryHandler<ClickHeatMapDataQuery, ClickHeatMapDataResult>
    {
        private ISecurityContext securityContext;

        public ClickHeatMapDataQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public ClickHeatMapDataResult Run(ISession session, ClickHeatMapDataQuery query)
        {
            var result = new ClickHeatMapDataResult();
            result.Screen = session.Query<Screen>()
                                .Where(s => s.Application.Id == query.AplicationId &&
                                            s.Path.ToLower() == query.Path.ToLower() &&
                                            s.Width == query.ScreenSize.Width &&
                                            s.Height == query.ScreenSize.Height)
                                .Select(s => new ScreenResult 
                                {
                                    Id = s.Id,
                                    Path = s.Path,
                                    ApplicationId = s.Application.Id,
                                    Height = s.Height,
                                    Width = s.Width,
                                    FileExtension = s.FileExtension
                                })
                                .FirstOrDefault();

            result.Data = session.Query<PageView>()
                    .Where(p => p.Application.Id == query.AplicationId &&
                                p.Path.ToLower() == query.Path.ToLower() &&
                                p.ScreenWidth == query.ScreenSize.Width &&
                                p.ScreenHeight == query.ScreenSize.Height &&
                                p.Date >= query.FromDate &&
                                p.Date <= query.ToDate)
                    .SelectMany(p => p.Clicks)
                    .GroupBy(c => new { X = c.X, Y = c.Y })
                    .Select(c => new ClickHeatMapItemResult { ClientX = c.Key.X, ClientY = c.Key.Y, Count = c.Count() })
                    .ToArray();

            return result;
        }
    }
}

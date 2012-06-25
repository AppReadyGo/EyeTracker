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
    public class ClickHeatMapDataQueryHandler : IQueryHandler<ClickHeatMapDataQuery, IEnumerable<ClickHeatMapDataResult>>
    {
        private ISecurityContext securityContext;

        public ClickHeatMapDataQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public IEnumerable<ClickHeatMapDataResult> Run(ISession session, ClickHeatMapDataQuery query)
        {
            var result = session.Query<PageView>()
                .Where(p => p.Application.Id == query.AplicationId &&
                            p.Path == query.Path &&
                            p.ClientWidth == query.ClientWidth &&
                            p.ClientHeight == query.ClientHeight &&
                            p.Date >= query.FromDate &&
                            p.Date <= query.ToDate)
                .SelectMany(p => p.Clicks)
                .GroupBy(c => new { X = c.X, Y = c.Y })
                .Select(c => new ClickHeatMapDataResult { ClientX = c.Key.X, ClientY = c.Key.Y, Count = c.Count() })
                .ToArray();

            return result;
        }
    }
}

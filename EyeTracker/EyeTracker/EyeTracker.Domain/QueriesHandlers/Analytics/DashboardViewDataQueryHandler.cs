using System;
using System.Collections.Generic;
using System.Linq;
using EyeTracker.Common;
using EyeTracker.Common.Queries.Analytics;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.QueryResults.Analytics;

namespace EyeTracker.Domain.Queries.Analytics
{
    public class DashboardViewDataQueryHandler : FilterBaseQueryHandler, IQueryHandler<DashboardViewDataQuery, DashboardViewDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public DashboardViewDataQueryHandler(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;  
            this.securityContext = securityContext;
        }

        public DashboardViewDataResult Run(ISession session, DashboardViewDataQuery query)
        {
            var res = GetResult<DashboardViewDataResult>(session, securityContext.CurrentUser.Id);
            int[] appIds = null;
            if (query.Applications.Any())
            {
                appIds = query.Applications.ToArray();
            }
            else
            {
                appIds = session.Query<Portfolio>()
                                .Where(p => p.Id == query.Portfolio)
                                .SelectMany(p => p.Applications)
                                .Select(a => a.Id)
                                .ToArray();
            }

            var dataQuery = session.Query<PageView>()
                                .Where(pv => appIds.Contains(pv.Application.Id) && pv.Date >= query.From && pv.Date <= query.To);

            if (query.ScreenSizes.Any())
            {
                var ss = query.ScreenSizes.ToArray();
                dataQuery = dataQuery.Where(pv => ss.Any(x => x.Height == pv.ClientHeight && x.Width == pv.ClientWidth));
            }

            if (query.Pathes.Any())
            {
                var p = query.Pathes.ToArray();
                dataQuery = dataQuery.Where(pv => p.Contains(pv.Path));
            }

            res.Data = dataQuery.GroupBy(g => g.Date.Date)
                                .Select(g => new KeyValuePair<DateTime, int>(g.Key, g.Count()))
                                .ToList().ToDictionary(v => v.Key, v => v.Value);

            res.ContentOverview = dataQuery.GroupBy(v => v.Path)
                                            .Select(g => new ContentOverviewResult
                                            {
                                                Path = g.Key,
                                                Views = g.Count()
                                            })
                                            .ToArray();
            return res;
        }
    }
}

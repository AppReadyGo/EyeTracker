using System;
using System.Collections.Generic;
using System.Linq;
using EyeTracker.Common;
using EyeTracker.Common.Queries.Analytics;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;

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
            int? applicationId = !query.ApplicationId.HasValue || query.ApplicationId.Value == 0 ? null : query.ApplicationId;

            var res = GetResult<DashboardViewDataResult>(session, securityContext.CurrentUser.Id);
            if (applicationId.HasValue)
            {
                res.Data = session.Query<PageView>()
                   .Where(pv => pv.Application.Id == applicationId.Value && pv.Date >= query.From && pv.Date <= query.To)
                   .GroupBy(g => g.Date)
                   .Select(g => new KeyValuePair<DateTime, int>(g.Key, g.Count()))
                   .ToList().ToDictionary(v => v.Key, v => v.Value);
            }
            else
            {
                var portfolio = session.Get<Portfolio>(query.PortfolioId);
                IEnumerable<int> appIds = portfolio.Applications.Select(a => a.Id).ToArray();

                res.Data = session.Query<PageView>()
                    .Where(pv => appIds.Contains(pv.Application.Id) && pv.Date >= query.From && pv.Date <= query.To)
                    .GroupBy(g => g.Date)
                    .Select(g => new KeyValuePair<DateTime, int>(g.Key, g.Count()))
                    .ToList().ToDictionary(v => v.Key, v => v.Value);
            }

            return res;
        }
    }
}

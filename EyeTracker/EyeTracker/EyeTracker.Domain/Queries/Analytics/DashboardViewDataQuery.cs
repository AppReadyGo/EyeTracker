using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Queries.Analytics;
using NHibernate.Linq;
using NHibernate;
using EyeTracker.Domain.Model;
using EyeTracker.Common;
using EyeTracker.Common.Queries.Analytics.QueryResults;

namespace EyeTracker.Domain.Queries.Analytics
{
    public class DashboardViewDataQuery : FilterQuery, IQueryHandler<DashboardViewData, DashboardViewDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public DashboardViewDataQuery(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;  
            this.securityContext = securityContext;
        }

        public DashboardViewDataResult Run(ISession session, DashboardViewData parameters)
        {
            int? applicationId = !parameters.ApplicationId.HasValue || parameters.ApplicationId.Value == 0 ? null : parameters.ApplicationId;

            var res = GetResult<DashboardViewDataResult>(session, securityContext.UserId);
            if (applicationId.HasValue)
            {
                res.Data = session.Query<PageView>()
                   .Where(pv => pv.Application.Id == applicationId.Value && pv.Date >= parameters.From && pv.Date <= parameters.To)
                   .GroupBy(g => g.Date)
                   .Select(g => new KeyValuePair<DateTime, int>(g.Key, g.Count()))
                   .ToList().ToDictionary(v => v.Key, v => v.Value);
            }
            else
            {
                var portfolio = session.Get<Portfolio>(parameters.PortfolioId);
                IEnumerable<int> appIds = portfolio.Applications.Select(a => a.Id).ToArray();

                res.Data = session.Query<PageView>()
                    .Where(pv => appIds.Contains(pv.Application.Id) && pv.Date >= parameters.From && pv.Date <= parameters.To)
                    .GroupBy(g => g.Date)
                    .Select(g => new KeyValuePair<DateTime, int>(g.Key, g.Count()))
                    .ToList().ToDictionary(v => v.Key, v => v.Value);
            }

            return res;
        }
    }
}

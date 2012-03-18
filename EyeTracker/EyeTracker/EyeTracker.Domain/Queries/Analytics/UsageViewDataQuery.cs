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
    public class UsageViewDataQuery : FilterQuery, IQueryHandler<UsageViewData, UsageViewDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public UsageViewDataQuery(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;
            this.securityContext = securityContext;
        }

        public UsageViewDataResult Run(ISession session, UsageViewData parameters)
        {
            var query = session.Query<PageView>();
            if (parameters.ApplicationId.HasValue)
            {
                query = query.Where(pv => pv.Application.Id == parameters.ApplicationId.Value);
            }
            else if (parameters.PortfolioId.HasValue)
            {
                query = query.Where(pv => pv.Application.Portfolio.Id == parameters.PortfolioId.Value);
            }
            var pageViews = query.Where(pv => pv.Date >= parameters.From && pv.Date <= parameters.To.Date).ToList();
            Dictionary<DateTime, int> result = null;
            switch (parameters.DataGrouping)
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

            var viewDataResult = GetResult<UsageViewDataResult>(session, securityContext.UserId);
            viewDataResult.Data = result;
            return viewDataResult;
        }
    }
}

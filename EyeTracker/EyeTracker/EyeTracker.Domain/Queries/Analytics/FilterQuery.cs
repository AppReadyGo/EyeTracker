using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Queries.Analytics;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Domain.Model;
using EyeTracker.Common.Queries.Analytics.QueryResults;

namespace EyeTracker.Domain.Queries.Analytics
{
    public abstract class FilterQuery
    {
        protected TResult GetResult<TResult>(ISession session, Guid userId)
            where TResult : FilterDataResult, new()
        {
            var filterData = new TResult();

            var portfolios = session.Query<Portfolio>()
                .Where(p => p.User.Id == userId)
                .Select(p => new
                {
                    key = p.Id,
                    details = new PortfolioResult
                    {
                        Id = p.Id,
                        Description = p.Description
                    }
                }).ToList();

            var portfoliosIds = portfolios.Select(p => p.key).ToArray();

            var applications = session.Query<Application>()
                .Where(a => portfoliosIds.Contains(a.Portfolio.Id))
                .Select(a => new
                {
                    key = a.Portfolio.Id,
                    app = new ApplicationResult
                    {
                        Id = a.Id,
                        Description = a.Description
                    }
                });

            foreach (var item in portfolios)
            {
                item.details.Applications = applications.Where(ai => ai.key == item.key).Select(ai => ai.app).ToList();
            }
            filterData.Portfolios = portfolios.Select(p => p.details);

            return filterData;
        }
    }
}

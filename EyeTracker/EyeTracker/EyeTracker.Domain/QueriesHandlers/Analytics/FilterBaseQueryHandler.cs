using System.Drawing;
using System.Linq;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using EyeTracker.Common.QueryResults.Application;
using EyeTracker.Common.QueryResults.Portfolio;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Queries.Analytics;

namespace EyeTracker.Domain.Queries.Analytics
{
    public abstract class FilterBaseQueryHandler
    {
        protected TResult GetResult<TResult>(ISession session, int userId, IFilterQuery query = null)
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

            var applications = session.Query<Model.Application>()
                .Where(a => portfoliosIds.Contains(a.Portfolio.Id))
                .Select(a => new
                {
                    key = a.Portfolio.Id,
                    app = new
                    {
                        Id = a.Id,
                        Description = a.Description
                    }
                })
                .ToArray();

            var sizes = session.Query<PageView>()
                                .Where(p => portfoliosIds.Contains(p.Application.Portfolio.Id))
                                .Select(p => new
                                {
                                    key = p.Application.Id,
                                    width = p.ScreenWidth,
                                    height = p.ScreenHeight
                                })
                                .ToArray().Distinct();

            var pathes = session.Query<PageView>()
                                .Where(p => portfoliosIds.Contains(p.Application.Portfolio.Id))
                                .Select(p => new
                                {
                                    key = p.Application.Id,
                                    path = p.Path
                                })
                                .ToArray().Distinct();

            foreach (var item in portfolios)
            {
                item.details.Applications = applications.Where(ai => ai.key == item.key)
                                                        .Select(ai => new ApplicationResult
                                                        {
                                                            Id = ai.app.Id,
                                                            Description = ai.app.Description,
                                                            ScreenSizes = sizes.Where(s => s.key == ai.app.Id)
                                                                            .Select(s => new Size(s.width, s.height))
                                                                            .ToArray(),
                                                            Pathes = pathes.Where(p => p.key == ai.app.Id)
                                                                            .Select(p => p.path)
                                                                            .ToArray()
                                                        }).ToArray();
            }

            filterData.Portfolios = portfolios.Select(p => p.details);

            if (query != null && query.ApplicationId.HasValue && !string.IsNullOrEmpty(query.Path) && query.ScreenSize.HasValue)
            {
                filterData.ScreenId = session.Query<Screen>()
                                    .Where(s => s.Application.Id == query.ApplicationId.Value &&
                                                s.Path.ToLower() == query.Path.ToLower() &&
                                                s.Width == query.ScreenSize.Value.Width &&
                                                s.Height == query.ScreenSize.Value.Height)
                                    .Select(s => (int?)s.Id)
                                    .FirstOrDefault();
            }

            return filterData;
        }
    }
}

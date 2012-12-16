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

            if (query != null)
            {
                filterData.SelectedApplicationId = query.ApplicationId;
                filterData.SelectedPath = query.Path;
                filterData.SelectedScreenSize = query.ScreenSize;
                ApplicationResult app = null;
                PortfolioResult portfolio = filterData.Portfolios.Single(p => p.Id == query.PortfolioId);

                app = filterData.SelectedApplicationId.HasValue ? portfolio.Applications.Single(a => a.Id == filterData.SelectedApplicationId.Value) : portfolio.Applications.First();

                if (app != null)
                {
                    filterData.SelectedApplicationId = app.Id;

                    if (string.IsNullOrEmpty(filterData.SelectedPath) && app.Pathes.Any())
                    {
                        filterData.SelectedPath = app.Pathes.First();
                    }
                    if (!filterData.SelectedScreenSize.HasValue && app.ScreenSizes.Any())
                    {
                        filterData.SelectedScreenSize = app.ScreenSizes.First();
                    }

                    if (!string.IsNullOrEmpty(filterData.SelectedPath) && filterData.SelectedScreenSize.HasValue)
                    {
                        filterData.ScreenData = new FilterDataResult.Screen();

                        var screen = session.Query<Screen>()
                                            .Where(s => s.Application.Id == filterData.SelectedApplicationId.Value &&
                                                        s.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                        s.Width == filterData.SelectedScreenSize.Value.Width &&
                                                        s.Height == filterData.SelectedScreenSize.Value.Height)
                                            .Select(s => new { Id = s.Id, FileExtention = s.FileExtension })
                                            .FirstOrDefault();
                        if (screen != null)
                        {
                            filterData.ScreenData.Id = screen.Id;
                            filterData.ScreenData.FileExtention = screen.FileExtention;
                        }

                        filterData.ScreenData.ClicksAmount = session.Query<Click>()
                                                            .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId.Value &&
                                                                        s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                        s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                        s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height &&
                                                                        s.PageView.Date >= query.From && s.PageView.Date <= query.To)
                                                            .Count();

                        filterData.ScreenData.HasClicks = session.Query<Click>()
                                                            .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId.Value &&
                                                                        s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                        s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                        s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height)
                                                            .Any();

                        filterData.ScreenData.ScrollsAmount = session.Query<Scroll>()
                                                            .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId.Value &&
                                                                        s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                        s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                        s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height &&
                                                                        s.PageView.Date >= query.From && s.PageView.Date <= query.To)
                                                            .Count();

                        filterData.ScreenData.HasScrolls = session.Query<Scroll>()
                                                            .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId.Value &&
                                                                        s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                        s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                        s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height)
                                                            .Any();

                    }
                }
            }

            return filterData;
        }
    }
}

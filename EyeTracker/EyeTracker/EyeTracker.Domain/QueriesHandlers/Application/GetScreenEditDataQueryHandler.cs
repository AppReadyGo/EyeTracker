using System.Linq;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common.QueryResults;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Queries.Application;
using EyeTracker.Common.QueryResults.Application;
using System.Drawing;

namespace EyeTracker.Domain.Queries.Application
{
    public class GetScreenEditDataQueryHandler : IQueryHandler<GetScreenEditDataQuery, ScreenDetailsDataResult>
    {
        public ScreenDetailsDataResult Run(ISession session, GetScreenEditDataQuery query)
        {
            var data = session.Query<Model.Screen>()
                        .Where(s => s.Id == query.Id)
                        .Select(s => new ScreenDetailsDataResult
                        {
                            Id = s.Id,
                            Path = s.Path,
                            Width = s.Width,
                            Height = s.Height,
                            FileExtention = s.FileExtension,
                            ApplicationId = s.Application.Id,
                            ApplicationDescription = s.Application.Description,
                            PortfolioId = s.Application.Portfolio.Id,
                            PortfolioDescription = s.Application.Portfolio.Description
                        })
                        .SingleOrDefault();

            if (data != null)
            {
                data.Pathes = session.Query<Model.PageView>()
                                        .Where(p => p.Application.Id == data.ApplicationId)
                                        .Select(p => p.Path)
                                        .Distinct()
                                        .ToArray();

                data.Sizes = session.Query<Model.PageView>()
                                        .Select(p => new { p.ScreenWidth, p.ScreenHeight })
                                        .ToArray()
                                        .GroupBy(p => new { p.ScreenWidth, p.ScreenHeight })
                                        .Select(g => new Size(g.Key.ScreenWidth, g.Key.ScreenHeight))
                                        .ToArray();
            }

            return data;
        }
    }
}

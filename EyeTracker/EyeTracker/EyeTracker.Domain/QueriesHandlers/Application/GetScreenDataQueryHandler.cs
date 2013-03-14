using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Queries;
using EyeTracker.Common.Queries.Application;
using EyeTracker.Common.QueryResults.Application;
using NHibernate;
using NHibernate.Linq;
using System.Drawing;

namespace EyeTracker.Domain.QueriesHandlers.Application
{
    public class GetScreenDataQueryHandler : IQueryHandler<GetScreenDataQuery, ScreenDataResult>
    {
        public ScreenDataResult Run(ISession session, GetScreenDataQuery query)
        {
            var data = session.Query<Model.Application>()
                        .Where(a => a.Id == query.Id)
                        .Select(a => new ScreenDataResult
                        {
                            ApplicationId = a.Id,
                            ApplicationDescription = a.Description,
                        })
                        .SingleOrDefault(); ;

            data.Pathes = session.Query<Model.PageView>()
                                    .Where(p => p.Application.Id == query.Id)
                                    .Select(p => p.Path)
                                    .Distinct()
                                    .ToArray();

            data.Sizes = session.Query<Model.PageView>()
                                    .Select(p => new { p.ScreenWidth, p.ScreenHeight })
                                    .ToArray()
                                    .GroupBy(p => new { p.ScreenWidth, p.ScreenHeight })
                                    .Select(g => new Size(g.Key.ScreenWidth, g.Key.ScreenHeight))
                                    .ToArray();

            return data;
        }
    }
}

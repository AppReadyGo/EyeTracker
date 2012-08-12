using System.Linq;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common.QueryResults;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Queries.Application;
using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Domain.Queries.Application
{
    public class GetScreenDetailsQueryHandler : IQueryHandler<GetScreenDetailsQuery, ScreenDetailsResult>
    {
        public ScreenDetailsResult Run(ISession session, GetScreenDetailsQuery query)
        {
            return session.Query<Model.Screen>()
                    .Where(s => s.Id == query.Id)
                    .Select(s => new ScreenDetailsResult
                    {
                        Id = s.Id,
                        Path = s.Path,
                        Width = s.Width,
                        Height = s.Height,
                        FileExtention = s.FileExtension,
                        ApplicationId = s.Application.Id,
                    })
                    .SingleOrDefault();
        }
    }
}

using System.Linq;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common.QueryResults;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Queries.Application;

namespace EyeTracker.Domain.Queries.Application
{
    public class GetApplicationDetailsQueryHandler : IQueryHandler<GetApplicationDetailsQuery, ApplicationDetailsResult>
    {
        public ApplicationDetailsResult Run(ISession session, GetApplicationDetailsQuery query)
        {
            return session.Query<Model.Application>()
                    .Where(a => a.Id == query.Id)
                    .Select(a => new ApplicationDetailsResult
                    {
                        Id = a.Id,
                        Description = a.Description,
                        Type = a.Type,
                        PortfolioId = a.Portfolio.Id,
                        PortfolioDescription = a.Portfolio.Description
                    })
                    .SingleOrDefault();
        }
    }
}

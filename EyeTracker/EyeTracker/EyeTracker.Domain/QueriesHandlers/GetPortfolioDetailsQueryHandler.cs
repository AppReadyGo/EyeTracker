//using System.Linq;
//using EyeTracker.Common.Queries.Users;
//using EyeTracker.Common.QueryResults;
//using EyeTracker.Common.QueryResults.Users;
//using EyeTracker.Domain.Model;
//using NHibernate;
//using NHibernate.Linq;

//namespace EyeTracker.Domain.Queries
//{
//    public class GetPortfolioDetailsQueryHandler : IQueryHandler<GetPortfolioDetailsQuery, PortfolioDetailsResult>
//    {
//        public PortfolioDetailsResult Run(ISession session, GetPortfolioDetailsQuery query)
//        {
//            return session.Query<Portfolio>()
//                    .Where(p => p.Id == query.Id)
//                    .Select(p => new PortfolioDetailsResult
//                    {
//                        Id = p.Id,
//                        Description = p.Description,
//                        TimeZone = p.TimeZone
//                    })
//                    .SingleOrDefault();
//        }
//    }
//}

using System.Linq;
using EyeTracker.Common;
using EyeTracker.Common.Queries.Analytics;
using EyeTracker.Common.QueryResults.Analytics;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries.Analytics
{
    public class PortfoliosQueryHandler : FilterBaseQueryHandler, IQueryHandler<PortfoliosQuery, PortfoliosDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public PortfoliosQueryHandler(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;
            this.securityContext = securityContext;
        }

        public PortfoliosDataResult Run(ISession session, PortfoliosQuery query)
        {
            var portfolios = session.Query<Portfolio>()
                        .Where(p => p.User.Id == securityContext.CurrentUser.Id)
                        .Select(p => new PortfolioResult
                        {
                            Id = p.Id,
                            Description = p.Description,
                        })
                        .ToArray();

            var applications = session.Query<Application>()
                                .Where(a => a.Portfolio.User.Id == securityContext.CurrentUser.Id)
                                .Select(a => new
                                {
                                    PortfolioId = a.Portfolio.Id,
                                    AppId = a.Id,
                                    AppDescription = a.Description
                                })
                                .ToArray();

            var visits = session.Query<PageView>()
                                .Where(p => p.Application.Portfolio.User.Id == securityContext.CurrentUser.Id)
                                .GroupBy(p => p.Application.Id)
                                .Select(g => new
                                {
                                    key = g.Key,
                                    visits = g.Sum(a => a.Id)
                                })
                                .ToArray();

            foreach (var portfolio in portfolios)
            {
                portfolio.Applications = applications
                                        .Where(a => a.PortfolioId == portfolio.Id)
                                        .Select(a => new ApplicationResult
                                        {
                                            Id = a.AppId,
                                            Description = a.AppDescription,
                                            Visits = visits.Where(v => v.key == a.AppId).Sum(v => v.visits)
                                        })
                                        .ToArray();
                portfolio.Visits = portfolio.Applications.Sum(a => a.Visits);
            }

            var viewDataResult = GetResult<PortfoliosDataResult>(session, securityContext.CurrentUser.Id);
            viewDataResult.PortfoliosData = portfolios;
            return viewDataResult;
        }
    }
}

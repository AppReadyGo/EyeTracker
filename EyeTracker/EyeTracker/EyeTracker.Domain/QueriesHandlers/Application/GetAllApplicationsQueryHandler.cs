using System;
using System.Linq;
using EyeTracker.Common;
using EyeTracker.Common.Queries.Analytics;
using EyeTracker.Common.QueryResults.Application;
using EyeTracker.Common.QueryResults.Portfolio;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Queries.Application;

namespace EyeTracker.Domain.Queries.Application
{
    public class GetAllApplicationsQueryHandler : IQueryHandler<GetAllApplicationsQuery, ApplicationsDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public GetAllApplicationsQueryHandler(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;
            this.securityContext = securityContext;
        }

        public ApplicationsDataResult Run(ISession session, GetAllApplicationsQuery query)
        {

            var res = session.Query<Portfolio>()
                              .Where(p => p.Id == query.PortfolioId && p.User.Id == securityContext.CurrentUser.Id)
                              .Select(p => new ApplicationsDataResult
                              {
                                  PortfolioId = p.Id,
                                  PortfolioDescription = p.Description
                              })
                              .Single();

            var applicationsQuery = session.Query<Model.Application>()
                        .Where(a => a.Portfolio.Id == query.PortfolioId && a.Portfolio.User.Id == securityContext.CurrentUser.Id);

            if (!string.IsNullOrEmpty(query.SearchStr))
            {
                applicationsQuery = applicationsQuery.Where(a => a.Description.ToLower().Contains(query.SearchStr.ToLower()));
            }

            res.Count = applicationsQuery.Count();
            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
            res.PageSize = query.PageSize;

            var applications = applicationsQuery.Select(a => new ApplicationDataItemResult
                                            {
                                                Id = a.Id,
                                                Description = a.Description,
                                                Type = a.Type
                                            })
                                            .ToArray();

            res.Applications = applications.Skip(res.PageSize * (res.CurPage - 1))
                                    .Take(res.PageSize)
                                    .ToArray();
            
            var visits = session.Query<PageView>()
                                .Where(p => p.Application.Portfolio.Id == query.PortfolioId && p.Application.Portfolio.User.Id == securityContext.CurrentUser.Id)
                                .GroupBy(p => p.Application.Id)
                                .Select(g => new
                                {
                                    Key = g.Key,
                                    VisitsCount = g.Count(),
                                    LastRecivedDataDate = g.Max(x => x.Date)
                                })
                                .ToArray();

            // Aplicatyion is not active if was not recived data for 3 days
            DateTime dt = DateTime.Now.AddDays(-3);

            foreach (var application in res.Applications)
            {
                var count = visits.SingleOrDefault(a => a.Key == application.Id);

                application.Visits = count != null ? count.VisitsCount : 0;

                application.IsActive = count != null && count.LastRecivedDataDate < dt ? true : false;
            }

            return res;
        }
    }
}

using System;
using System.Linq;
using System.Reflection;
using EyeTracker.Common;
using EyeTracker.Common.Logger;
using EyeTracker.Common.Queries.Application;
using EyeTracker.Common.QueryResults.Application;
using EyeTracker.Domain.Model;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries.Application
{
    public class GetAllApplicationsQueryHandler : IQueryHandler<GetAllApplicationsQuery, ApplicationsDataResult>
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        private ISecurityContext securityContext;

        public GetAllApplicationsQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public ApplicationsDataResult Run(ISession session, GetAllApplicationsQuery query)
        {
            log.WriteInformation("-> Get all applications for portfolio:{0}, SearchStr:{1}, PageSize:{2}, CurPage:{3}, User:{4}", query.PortfolioId, query.SearchStr, query.PageSize, query.CurPage, securityContext.CurrentUser.Email);
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
            log.WriteInformation("Get all applications for portfolio ->");

            return res;
        }
    }
}

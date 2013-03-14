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
using System.Collections.Generic;
using System.Drawing;

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
            log.WriteInformation("-> Get all applications for SearchStr:{0}, PageSize:{1}, CurPage:{2}, User:{3}", query.SearchStr, query.PageSize, query.CurPage, securityContext.CurrentUser.Email);
            var res = new ApplicationsDataResult();

            var applicationsQuery = session.Query<Model.Application>()
                        .Where(a => a.User.Id == securityContext.CurrentUser.Id);

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

            applications = applications.Skip(res.PageSize * (res.CurPage - 1))
                                    .Take(res.PageSize)
                                    .ToArray();
            
            //var visits = session.Query<PageView>()
            //                    .Where(p => p.Application.User.Id == securityContext.CurrentUser.Id)
            //                    .GroupBy(p => p.Application.Id)
            //                    .Select(g => new
            //                    {
            //                        Key = g.Key,
            //                        VisitsCount = g.Count(),
            //                        LastRecivedDataDate = g.Max(x => x.Date)
            //                    })
            //                    .ToArray();

            // Get top applications and top screens
            var visitsByScreens = session.Query<PageView>()
                    .Where(p => p.Application.User.Id == securityContext.CurrentUser.Id)
                    .GroupBy(p => new
                    {
                        ApplicationId = p.Application.Id,
                        Description = p.Application.Description,
                        Path = p.Path,
                        ScreenHeight = p.ScreenHeight,
                        ScreenWidth = p.ScreenWidth
                    })
                    .Select(g => new
                    {
                        Key = g.Key,
                        VisitsCount = g.Count(),
                        LastRecivedDataDate = g.Max(x => x.Date)
                    })
                    .ToArray();

            var visits = visitsByScreens.GroupBy(g => new
                                {
                                    ApplicationId = g.Key.ApplicationId,
                                    Description = g.Key.Description,
                                })
                                .Select(g => new
                                {
                                    Key = g.Key,
                                    VisitsCount = g.Count(),
                                    LastRecivedDataDate = g.Max(x => x.LastRecivedDataDate)
                                })
                                .ToArray();

            res.TopScreens = visitsByScreens.OrderByDescending(g => g.VisitsCount)
                                            .Take(5)
                                            .Select(g => new ApplicationScreenResult
                                            {
                                                ApplicationId = g.Key.ApplicationId,
                                                Path = g.Key.Path,
                                                ScreenSize = new Size(g.Key.ScreenWidth, g.Key.ScreenHeight)
                                            }).ToArray();

            res.TopApplications = visits.OrderByDescending(g => g.VisitsCount)
                                            .Take(5)
                                            .Select(g => new ApplicationResult
                                            {
                                                Id = g.Key.ApplicationId,
                                                Description = g.Key.Description
                                            }).ToArray();

            // Aplicatyion is not active if was not recived data for 3 days
            DateTime dt = DateTime.Now.AddDays(-3);

            foreach (var application in res.Applications)
            {
                var count = visits.SingleOrDefault(a => a.Key.ApplicationId == application.Id);

                application.Visits = count != null ? count.VisitsCount : 0;

                application.IsActive = count != null && count.LastRecivedDataDate < dt ? true : false;
            }
            log.WriteInformation("Get all applications for portfolio ->");

            return res;
        }
    }
}

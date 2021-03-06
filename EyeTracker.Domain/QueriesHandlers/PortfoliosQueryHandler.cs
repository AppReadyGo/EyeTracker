﻿//using System.Linq;
//using EyeTracker.Common;
//using EyeTracker.Common.Queries.Analytics;
//using EyeTracker.Common.QueryResults.Analytics;
//using EyeTracker.Domain.Model;
//using NHibernate;
//using NHibernate.Linq;
//using EyeTracker.Common.QueryResults.Portfolio;
//using System;
//using System.Drawing;
//using EyeTracker.Common.QueryResults.Application;

//namespace EyeTracker.Domain.Queries
//{
//    public class PortfoliosQueryHandler : IQueryHandler<PortfoliosQuery, PortfoliosDataResult>
//    {
//        private IRepository repository;
//        private ISecurityContext securityContext;

//        public PortfoliosQueryHandler(IRepository repository, ISecurityContext securityContext)
//        {
//            this.repository = repository;
//            this.securityContext = securityContext;
//        }

//        public PortfoliosDataResult Run(ISession session, PortfoliosQuery query)
//        {
//            var res = new PortfoliosDataResult();

//            var portfoliosQuery = session.Query<Portfolio>()
//                        .Where(p => p.User.Id == securityContext.CurrentUser.Id);

//            if (!string.IsNullOrEmpty(query.SearchStr))
//            {
//                portfoliosQuery = portfoliosQuery.Where(p => p.Description.Contains(query.SearchStr));
//            }

//            res.Count = portfoliosQuery.Count();
//            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
//            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
//            res.PageSize = query.PageSize;

//            var portfolios = portfoliosQuery.Select(p => new PortfolioDataItemResult
//                                            {
//                                                Id = p.Id,
//                                                Description = p.Description,
//                                            })
//                                            .ToArray();

//            res.Portfolios = portfolios.Skip(res.PageSize * (res.CurPage - 1))
//                                    .Take(res.PageSize)
//                                    .ToArray();

//            var applications = session.Query<Model.Application>()
//                                .Where(a => a.User.Id == securityContext.CurrentUser.Id)
//                                .Select(a => new ApplicationDataItemResult
//                                {
//                                    Id = a.Id,
//                                    Description = a.Description,
//                                    Type = a.Type
//                                })
//                                .ToArray();

//            var visitsByScreens = session.Query<PageView>()
//                                .Where(p => p.Application.User.Id == securityContext.CurrentUser.Id)
//                                .GroupBy(p => new
//                                {
//                                    PortfolioId = p.Application.Portfolio.Id,
//                                    ApplicationId = p.Application.Id,
//                                    Description = p.Application.Description,
//                                    Path = p.Path,
//                                    ScreenHeight = p.ScreenHeight,
//                                    ScreenWidth = p.ScreenWidth
//                                })
//                                .Select(g => new
//                                {
//                                    Key = g.Key,
//                                    VisitsCount = g.Count(),
//                                    LastRecivedDataDate = g.Max(x => x.Date)
//                                })
//                                .ToArray();

//            var visits = visitsByScreens.GroupBy(g => new
//                                {
//                                    PortfolioId = g.Key.PortfolioId,
//                                    ApplicationId = g.Key.ApplicationId,
//                                    Description = g.Key.Description,
//                                })
//                                .Select(g => new
//                                {
//                                    Key = g.Key,
//                                    VisitsCount = g.Count(),
//                                    LastRecivedDataDate = g.Max(x => x.LastRecivedDataDate)
//                                })
//                                .ToArray();

//            res.TopScreens = visitsByScreens.OrderByDescending(g => g.VisitsCount).Take(5).Select(g => new TopScreensItemResult
//            {
//                ApplicationId = g.Key.ApplicationId,
//                PortfolioId = g.Key.PortfolioId,
//                Path = g.Key.Path,
//                ScreenSize = new Size(g.Key.ScreenWidth, g.Key.ScreenHeight)
//            }).ToArray();

//            res.TopApplications = visits.OrderByDescending(g => g.VisitsCount).Take(5).Select(g => new TopApplicationsItemResult
//                {
//                    PortfolioId = g.Key.PortfolioId,
//                    Id = g.Key.ApplicationId,
//                    Description = g.Key.Description
//                }).ToArray();

//            // Aplicatyion is not active if was not recived data for 3 days
//            DateTime dt = DateTime.Now.AddDays(-3);

//            foreach (var portfolio in res.Portfolios)
//            {
//                portfolio.Applications = applications.Where(a => a.PortfolioId == portfolio.Id).Cast<ApplicationDataItemResult>().ToArray();

//                foreach (var app in portfolio.Applications)
//                {
//                    var appVisits = visits.Where(a => a.Key.ApplicationId == app.Id);

//                    app.Visits = appVisits.Any() ? appVisits.Sum(x => x.VisitsCount) : 0;
//                    portfolio.Visits += app.Visits;

//                    app.IsActive = appVisits.Any(x => x.LastRecivedDataDate > dt) ? true : false;
//                }

//                portfolio.IsActive = visits.Any(x => x.Key.PortfolioId == portfolio.Id && x.LastRecivedDataDate > dt) ? true : false;
//            }

//            return res;
//        }
//    }
//}

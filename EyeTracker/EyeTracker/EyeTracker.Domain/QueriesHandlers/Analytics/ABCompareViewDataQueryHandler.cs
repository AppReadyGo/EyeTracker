using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Queries.Analytics;
using NHibernate.Linq;
using NHibernate;
using EyeTracker.Domain.Model;
using EyeTracker.Common;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using EyeTracker.Common.Commands;
using EyeTracker.Common.QueryResults.Analytics;

namespace EyeTracker.Domain.Queries.Analytics
{
    public class ABCompareViewDataQueryHandler : FilterBaseQueryHandler, IQueryHandler<ABCompareViewDataQuery, ABCompareViewDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public ABCompareViewDataQueryHandler(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;
            this.securityContext = securityContext;
        }

        public ABCompareViewDataResult Run(ISession session, ABCompareViewDataQuery query)
        {
            var data = GetResult<ABCompareViewDataResult>(session, this.securityContext.CurrentUser.Id, query);
            data.SelectedSecondPath = string.IsNullOrEmpty(query.Path) ? data.SelectedPath : query.Path;
            data.SecondFilteredClicks = session.Query<Click>()
                                        .Where(s => s.PageView.Application.Id == data.SelectedApplicationId.Value &&
                                                    s.PageView.Path.ToLower() == data.SelectedSecondPath.ToLower() &&
                                                    s.PageView.ScreenWidth == data.SelectedScreenSize.Value.Width &&
                                                    s.PageView.ScreenHeight == data.SelectedScreenSize.Value.Height &&
                                                    s.PageView.Date >= query.From && s.PageView.Date <= query.To)
                                                    .Select(s => s.Id)
                                                    .ToArray()
                                                    .Count();

            data.SecondHasClicks = session.Query<Click>()
                                                    .Where(s => s.PageView.Application.Id == data.SelectedApplicationId.Value &&
                                                                s.PageView.Path.ToLower() == data.SelectedSecondPath.ToLower() &&
                                                                s.PageView.ScreenWidth == data.SelectedScreenSize.Value.Width &&
                                                                s.PageView.ScreenHeight == data.SelectedScreenSize.Value.Height)
                                                    .Any();

            data.SecondFilteredScrolls = session.Query<Scroll>()
                                        .Where(s => s.PageView.Application.Id == data.SelectedApplicationId.Value &&
                                                    s.PageView.Path.ToLower() == data.SelectedSecondPath.ToLower() &&
                                                    s.PageView.ScreenWidth == data.SelectedScreenSize.Value.Width &&
                                                    s.PageView.ScreenHeight == data.SelectedScreenSize.Value.Height &&
                                                    s.PageView.Date >= query.From && s.PageView.Date <= query.To)
                                                    .Select(s => s.Id)
                                                    .ToArray()
                                                    .Count();
            return data;
        }
    }
}

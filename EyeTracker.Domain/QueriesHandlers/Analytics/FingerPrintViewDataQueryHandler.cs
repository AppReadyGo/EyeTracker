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
    public class FingerPrintViewDataQueryHandler : FilterBaseQueryHandler, IQueryHandler<FingerPrintViewDataQuery, FingerPrintViewDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public FingerPrintViewDataQueryHandler(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;
            this.securityContext = securityContext;
        }

        public FingerPrintViewDataResult Run(ISession session, FingerPrintViewDataQuery query)
        {
            var data = GetResult<FingerPrintViewDataResult>(session, this.securityContext.CurrentUser.Id, query);
            data.Screens = session.Query<Screen>()
                                        .Where(s => s.Application.Id == query.ApplicationId.Value)
                                        .Select(s => new ScreenResult
                                        {
                                            Id = s.Id,
                                            Path = s.Path,
                                            ApplicationId = s.Application.Id,
                                            Height = s.Height,
                                            Width = s.Width,
                                            FileExtension = s.FileExtension
                                        })
                                        .ToArray();
            if(data.ScreenId.HasValue)
            {
                data.ScreenFileExtention = session.Query<Screen>()
                                        .Where(s => s.Id == data.ScreenId.Value)
                                        .Select(s => s.FileExtension)
                                        .FirstOrDefault();
                data.PointsOnReport = session.Query<PageView>()
                                        .Where(p => p.Application.Id == query.ApplicationId &&
                                                    p.Path.ToLower() == query.Path.ToLower() &&
                                                    p.ScreenWidth == query.ScreenSize.Value.Width &&
                                                    p.ScreenHeight == query.ScreenSize.Value.Height &&
                                                    p.Date >= query.From &&
                                                    p.Date <= query.To)
                                        .SelectMany(p => p.Clicks)
                                        .Count();
            }
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Queries.Analytics;
using NHibernate.Linq;
using NHibernate;
using EyeTracker.Domain.Model;
using EyeTracker.Common;
using EyeTracker.Common.Queries.Analytics.QueryResults;

namespace EyeTracker.Domain.Queries.Analytics
{
    public class FingerPrintViewDataQuery : FilterQuery, IQueryHandler<FingerPrintViewData, FingerPrintViewDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public FingerPrintViewDataQuery(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;
            this.securityContext = securityContext;
        }

        public FingerPrintViewDataResult Run(ISession session, FingerPrintViewData parameters)
        {
            return GetResult<FingerPrintViewDataResult>(session, securityContext.UserId);
        }
    }
}

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
    public class FilterQueryHandler : FilterBaseQueryHandler, IQueryHandler<FilterQuery, FilterDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public FilterQueryHandler(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;  
            this.securityContext = securityContext;
        }

        public FilterDataResult Run(ISession session, FilterQuery query)
        {
            int? applicationId = !query.ApplicationId.HasValue || query.ApplicationId.Value == 0 ? null : query.ApplicationId;

            var res = GetResult<FilterDataResult>(session, securityContext.UserId);

            return res;
        }
    }
}

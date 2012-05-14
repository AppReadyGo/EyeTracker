using EyeTracker.Common.Queries.Analytics;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using NHibernate;
using EyeTracker.Common.Commands;

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

            var res = GetResult<FilterDataResult>(session, securityContext.CurrentUser.Id);

            return res;
        }
    }
}

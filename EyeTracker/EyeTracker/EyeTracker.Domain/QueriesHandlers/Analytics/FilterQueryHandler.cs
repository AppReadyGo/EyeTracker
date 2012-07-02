using EyeTracker.Common;
using EyeTracker.Common.Queries.Analytics;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using NHibernate;

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
            return GetResult<FilterDataResult>(session, securityContext.CurrentUser.Id);
        }
    }
}

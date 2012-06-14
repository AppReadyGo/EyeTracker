using System.Collections.Generic;
using System.Linq;
using EyeTracker.Common.Queries;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Domain.Model.Users;
using EyeTracker.Common;

namespace EyeTracker.Domain.Queries
{
    public class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery, IEnumerable<StaffRole>>
    {
        public IEnumerable<StaffRole> Run(ISession session, GetUserRolesQuery query)
        {
            return session.Query<Staff>()
                    .Where(u => u.Id == query.Id)
                    .SelectMany(u => u.Roles)
                    .Select(r => r)
                    .ToArray();
        }
    }
}

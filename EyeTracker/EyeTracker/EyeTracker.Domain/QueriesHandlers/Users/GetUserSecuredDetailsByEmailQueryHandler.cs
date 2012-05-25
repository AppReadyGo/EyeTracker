using System.Collections.Generic;
using System.Linq;
using EyeTracker.Common.Queries;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Domain.Model.Users;

namespace EyeTracker.Domain.Queries
{
    public class GetUserSecuredDetailsByEmailQueryHandler : IQueryHandler<GetUserSecuredDetailsByEmailQuery, UserSecuredDetailsResult>
    {
        public UserSecuredDetailsResult Run(ISession session, GetUserSecuredDetailsByEmailQuery query)
        {
            return session.Query<User>()
                    .Where(u => u.Email.ToLower() == query.Email.ToLower())
                    .Select(u => new UserSecuredDetailsResult
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Password = u.Password,
                        PasswordSalt = u.PasswordSalt,
                        Activated = u.Activated
                    })
                    .SingleOrDefault();
        }
    }
}

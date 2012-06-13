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
            var user = session.Query<User>()
                    .Where(u => u.Email.ToLower() == query.Email.ToLower())
                    .Select(u => new
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Password = u.Password,
                        PasswordSalt = u.PasswordSalt,
                        Activated = u.Activated,
                        Type = u.Type
                    })
                    .SingleOrDefault();
            if (user != null)
            {
                return new UserSecuredDetailsResult
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    PasswordSalt = user.PasswordSalt,
                    Activated = user.Activated,
                    Roles = user.Type == EyeTracker.Common.UserType.Staff ? session.Query<Staff>()
                                                                                    .Where(u => u.Id == user.Id)
                                                                                    .SelectMany(u => u.Roles)
                                                                                    .Select(r => r)
                                                                                    .ToArray() : null
                };
            }
            else
            {
                return null;
            }
        }
    }
}

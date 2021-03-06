﻿using System.Collections.Generic;
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
    public class GetUserDetailsByIdQueryHandler : IQueryHandler<GetUserDetailsByIdQuery, UserDetailsResult>
    {
        public UserDetailsResult Run(ISession session, GetUserDetailsByIdQuery query)
        {
            return session.Query<User>()
                    .Where(u => u.Id == query.Id)
                    .Select(u => new UserDetailsResult
                    {
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    })
                    .SingleOrDefault();
        }
    }
}

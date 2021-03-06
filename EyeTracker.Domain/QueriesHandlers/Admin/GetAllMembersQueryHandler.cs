﻿using System.Collections.Generic;
using System.Linq;
using EyeTracker.Common.Queries;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Domain.Model.Users;
using EyeTracker.Common.Queries.Admin;
using EyeTracker.Common;
using NHibernate.Transform;
using EyeTracker.Domain.QueriesHandlers.Admin;

namespace EyeTracker.Domain.Queries.Admin
{
    public class GetAllMembersQueryHandler : IQueryHandler<GetAllMembersQuery, AllMembersResult>
    {
        public AllMembersResult Run(ISession session, GetAllMembersQuery query)
        {
            var res = new AllMembersResult();

            var usersQuery = session.Query<User>()
                        .Where(u => u.Type == UserType.Member);

            res.Count = usersQuery.Count();
            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
            res.PageSize = query.PageSize;


            var users = usersQuery.Select(u => new UserFullDetailsResult
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Activated = u.Activated,
                Id = u.Id,
                SpecialAccess = u.SpecialAccess,
                CreateDate = u.CreateDate,
                LastAccessDate = u.LastAccessDate
            });

            if (query.OrderBy == GetAllMembersQuery.OrderByColumn.Email)
            {
                users = query.ASC ? users.OrderBy(u => u.Email) : users.OrderByDescending(u => u.Email);
            }
            else if (query.OrderBy == GetAllMembersQuery.OrderByColumn.Name)
            {
                users = query.ASC ? users.OrderBy(u => u.LastName + " " + u.FirstName) : users.OrderByDescending(u => u.Email);
            }
            else if (query.OrderBy == GetAllMembersQuery.OrderByColumn.CreateDate)
            {
                users = query.ASC ? users.OrderBy(u => u.CreateDate) : users.OrderByDescending(u => u.CreateDate);
            }

            res.Users = users.Skip(res.PageSize * (res.CurPage - 1))
                        .Take(res.PageSize)
                        .ToArray();

            return res;
        }
    }
}

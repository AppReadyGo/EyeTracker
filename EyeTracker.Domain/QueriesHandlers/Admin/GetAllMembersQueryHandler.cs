using System.Collections.Generic;
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

            res.Users = usersQuery.Select(u => new UserFullDetailsResult
                    {
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Activated = u.Activated,
                        Id = u.Id,
                        SpecialAccess = u.SpecialAccess
                    })
                    .Skip(res.PageSize * res.CurPage)
                    .Take(res.PageSize)
                    .ToArray();

            return res;
        }
    }
}

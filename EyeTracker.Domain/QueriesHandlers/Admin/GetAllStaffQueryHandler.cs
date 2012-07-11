using System.Linq;
using EyeTracker.Common;
using EyeTracker.Common.Queries.Admin;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Domain.Model.Users;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;

namespace EyeTracker.Domain.Queries.Admin
{
    public class GetAllStaffQueryHandler : IQueryHandler<GetAllStaffQuery, AllStaffResult>
    {
        public AllStaffResult Run(ISession session, GetAllStaffQuery query)
        {
            var res = new AllStaffResult();

            var usersQuery = session.Query<User>()
                        .Where(u => u.Type == UserType.Staff);

            res.Count = usersQuery.Count();
            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
            res.PageSize = query.PageSize;

            res.Users = usersQuery.Select(u => new StaffFullDetailsResult
                        {
                            Email = u.Email,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Activated = u.Activated,
                            Id = u.Id,
                            LastAccessDate = u.LastAccessDate
                        })
                        .Skip(res.PageSize * (res.CurPage - 1))
                        .Take(res.PageSize)
                        .ToArray();

            var userIds = res.Users.Select(u => u.Id).ToArray();

            UserRole userRole = null;
            StaffRole staffRole = StaffRole.Administrator;
            var userRoles = session.QueryOver<Staff>()
                                .JoinAlias(u => u.Roles, () => staffRole)
                                .Where(u => u.Id.IsIn(userIds))
                                .SelectList(list => list
                                .Select(u => u.Id).WithAlias(() => userRole.Id)
                                .Select(u => staffRole).WithAlias(() => userRole.Role))
                                .TransformUsing(Transformers.AliasToBean<UserRole>())
                                .List<UserRole>();

            foreach (var u in res.Users)
            {
                u.Roles = userRoles.Where(x => x.Id == u.Id).Select(x => x.Role);
            }

            return res;
        }

        private class UserRole
        {
            public int Id { get; set; }

            public StaffRole Role { get; set; }
        }
    }
}

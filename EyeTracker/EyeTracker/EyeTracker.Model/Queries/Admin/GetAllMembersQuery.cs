using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Users;

namespace EyeTracker.Common.Queries.Admin
{
    public class GetAllMembersQuery : IQuery<AllMembersResult>
    {
        public int CurPage { get; set; }

        public int PageSize { get; set; }

        public GetAllMembersQuery(int curPage, int pageSize)
        {
            this.CurPage = curPage;
            this.PageSize = pageSize;
        }
    }
}

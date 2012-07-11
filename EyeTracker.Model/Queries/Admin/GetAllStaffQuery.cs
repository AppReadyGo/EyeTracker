using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Users;

namespace EyeTracker.Common.Queries.Admin
{
    public class GetAllStaffQuery/*<TSource, TKey>*/ : IQuery<AllStaffResult>
    {
        public int CurPage { get; set; }

        public int PageSize { get; set; }

        //public Func<TSource, TKey> KeySelector { get; set; }

        public GetAllStaffQuery(string searchStr/*, Func<TSource, TKey> keySelector*/,int curPage, int pageSize)
        {
            this.CurPage = curPage;
            this.PageSize = pageSize;
        }
    }
}

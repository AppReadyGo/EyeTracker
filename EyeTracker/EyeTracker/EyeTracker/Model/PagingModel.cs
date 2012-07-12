using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model
{
    public class PagingModel
    {
        public bool IsOnePage { get; set; }

        public int? PreviousPage { get; set; }

        public int? NextPage { get; set; }

        public int Count { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<StaffUserDetailsModel> Users { get; set; }

        public int CurPage { get; set; }

        public string SearchStrUrlPart { get; set; }

        public string SearchStr { get; set; }
    }

    public class StaffUserDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Index { get; set; }

        public bool IsAlternative { get; set; }

        public string Roles { get; set; }

        public bool Activated { get; set; }

        public string Email { get; set; }

        public string LastAccess { get; set; }
    }
}
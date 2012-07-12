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

        public int CurPage { get; set; }

        public string UrlPart { get; set; }

        public string SearchStr { get; set; }

        public string SearchStrUrlPart { get; set; }
    }

    public class StaffPagingModel : PagingModel
    {
        public IEnumerable<StaffDetailsModel> Users { get; set; }

        public string EmailOrder { get; set; }

        public string NameOrder { get; set; }
    }

    public class MembersPagingModel : PagingModel
    {
        public IEnumerable<MemberDetailsModel> Users { get; set; }

        public string EmailOrder { get; set; }

        public string NameOrder { get; set; }
    }

    public class StaffDetailsModel
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

    public class MemberDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Index { get; set; }

        public bool IsAlternative { get; set; }

        public bool Activated { get; set; }

        public string Email { get; set; }

        public string LastAccess { get; set; }

        public string Registred { get; set; }

        public bool SpecialAccess { get; set; }
    }
}
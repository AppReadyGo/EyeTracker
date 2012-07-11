using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Users
{
    public class UserFullDetailsResult : UserDetailsResult
    {
        public int Id { get; set; }

        public bool Activated { get; set; }

        public bool SpecialAccess { get; set; }

        public DateTime? LastAccessDate { get; set; }
    }
}

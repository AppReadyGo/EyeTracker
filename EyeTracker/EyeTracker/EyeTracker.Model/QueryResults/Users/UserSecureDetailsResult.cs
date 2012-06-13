using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Users
{
    public class UserSecuredDetailsResult
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public bool Activated { get; set; }

        public int Id { get; set; }

        public IEnumerable<StaffRole> Roles { get; set; }
    }
}

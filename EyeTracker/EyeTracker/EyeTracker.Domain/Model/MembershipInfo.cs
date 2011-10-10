using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class MembershipInfo
    {
        public IList<Application> Applications { get; set; }
        public IList<Role> Roles { get; set; }
        public IList<User> Users { get; set; }
    }
}

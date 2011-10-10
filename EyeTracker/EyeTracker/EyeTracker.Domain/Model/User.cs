using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class User : Entity
    {
        public virtual DateTime LastActivityDate { get; set; }
        public virtual string Email { get; set; }
        public virtual int TimeZone { get; set; }
        public virtual IList<Role> Roles { get; set; }
        public virtual Application App { get; set; }
        public virtual AccountProfiler Profiler { get; set; }
    }
}

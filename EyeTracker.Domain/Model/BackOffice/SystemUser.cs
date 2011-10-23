using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.BackOffice
{
    public class SystemUser : Entity
    {
        public virtual DateTime LastActivityDate { get; set; }
        public virtual string Email { get; set; }
        public virtual int TimeZone { get; set; }
        public virtual IList<SystemRole> Roles { get; set; }
        public virtual SystemApplication Application { get; set; }
        public virtual AccountProfiler Profiler { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.BackOffice
{
    public class SystemApplication : Entity
    {
        public virtual string Description { get; set; }
        public virtual IList<SystemRole> Roles { get; set; }
        public virtual IList<SystemUser> Users { get; set; }
    }
}

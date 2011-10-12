using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.BackOffice
{
    public class SystemRole : Entity
    {
        public virtual string Description { get; set; }
        public virtual SystemApplication App { get; set; }
        public virtual IList<SystemUser> Users { get; set; }
    }
}

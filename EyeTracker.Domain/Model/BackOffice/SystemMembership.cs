using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.BackOffice
{
    public class SystemMembership
    {
        public virtual SystemUser User { get; set; }
        public virtual string Email { get; set; }
    }
}

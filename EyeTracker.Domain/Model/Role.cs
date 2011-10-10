using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class Role : Entity
    {
        public virtual string Description { get; set; }
        public virtual Application App { get; set; }
        public virtual IList<User> Users { get; set; }
    }
}

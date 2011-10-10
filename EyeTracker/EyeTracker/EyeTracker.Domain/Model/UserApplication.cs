using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class UserApplication
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual User User { get; set; }
    }
}

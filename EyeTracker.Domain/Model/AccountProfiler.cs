using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.BackOffice;
using EyeTracker.Domain.Model.Users;

namespace EyeTracker.Domain.Model
{
    public class AccountProfiler
    {
        public virtual int Id { get; set; }
        public virtual int UpdateFriquency { get; set; }
        public virtual decimal Price { get; set; }
        public virtual IList<User> Users { get; set; }
    }
}

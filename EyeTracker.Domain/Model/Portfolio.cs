using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Model
{
    public class Portfolio
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual Country Country { get; set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual SystemUser User { get; set; }
        public virtual IList<Application> Applications { get; set; }
    }
}

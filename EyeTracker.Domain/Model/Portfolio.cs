using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Model
{
    public class Portfolio
    {
        public virtual int Id { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual Country Country { get; protected set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual SystemUser User { get; protected set; }
        public virtual IList<Application> Applications { get; protected set; }

        public Portfolio()
        {
            CreateDate = DateTime.UtcNow;
        }

        public Portfolio(string description, Country country, SystemUser user) : this()
        {
            this.Description = description;
            this.Country = country;
            this.User = user;
        }
    }
}

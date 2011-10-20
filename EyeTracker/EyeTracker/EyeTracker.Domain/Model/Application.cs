using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Model
{
    public class Application
    {
        public virtual int Id { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual ApplicationType Type { get; protected set; }
        public virtual IList<Portfolio> Portfolios { get; protected set; }

        public Application()
        {
        }

        public Application(Portfolio portfolio, string description, ApplicationType type)
        {
            this.CreateDate = DateTime.UtcNow;
            this.Description = description;
            this.Type = type;
            this.Portfolios = new List<Portfolio>() { portfolio };
        }

        protected internal virtual void Update(string description, ApplicationType type)
        {
            this.Description = description;
            this.Type = type;
        }
    }
}

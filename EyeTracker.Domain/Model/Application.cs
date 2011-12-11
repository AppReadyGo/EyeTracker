using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Model
{
    public class Application
    {
        public virtual long Id { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual ApplicationType Type { get; protected set; }
        public virtual Portfolio Portfolio { get; protected set; }
        public virtual IEnumerable<Screen> Screens { get; set; }

        public Application()
        {
            Screens = new List<Screen>();
        }

        public Application(Portfolio portfolio, string description, ApplicationType type)
        {
            this.CreateDate = DateTime.UtcNow;
            this.Description = description;
            this.Type = type;
            this.Portfolio = portfolio;
        }

        protected internal virtual void Update(string description)
        {
            this.Description = description;
        }
    }
}

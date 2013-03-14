//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using EyeTracker.Domain.Model.BackOffice;
//using Iesi.Collections.Generic;
//using NHibernate.Collection.Generic;
//using EyeTracker.Domain.Model.Users;

//namespace EyeTracker.Domain.Model
//{
//    public class Portfolio
//    {
//        private PersistentGenericSet<Application> applications = null;

//        public virtual int Id { get; protected set; }
//        public virtual string Description { get; protected set; }
//        public virtual int TimeZone { get; protected set; }
//        public virtual DateTime CreateDate { get; protected set; }
//        public virtual User User { get; protected set; }
//        public virtual IEnumerable<Application> Applications { get { return applications; } }

//        public Portfolio()
//        {
//            CreateDate = DateTime.UtcNow;
//        }

//        public Portfolio(string description, int timeZone, User user)
//            : this()
//        {
//            this.Description = description;
//            this.TimeZone = timeZone;
//            this.User = user;
//        }

//        public virtual void Update(string description, int timeZone)
//        {
//            Description = description;
//            TimeZone = timeZone;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Model
{

    /// <summary>
    /// this class represents application data mode 
    /// instance of this class holds information about one single applications 
    /// and describes application properties 
    /// </summary>
    public class Application
    {
        /// <summary>
        /// application ID 
        /// </summary>
        public virtual int Id { get; protected set; }
        /// <summary>
        /// Application description
        /// </summary>
        public virtual string Description { get; protected set; }
        /// <summary>
        /// Application creation date - when the application created in our system 
        /// </summary>
        public virtual DateTime CreateDate { get; protected set; }
        /// <summary>
        /// Member to represent OS platform type 
        /// </summary>
        public virtual ApplicationType Type { get; protected set; }
        /// <summary>
        /// this member holds Portfolio 
        /// </summary>
        public virtual Portfolio Portfolio { get; protected set; }
        /// <summary>
        /// holds application screens list 
        /// </summary>
        public virtual IEnumerable<Screen> Screens { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Application()
        {
            Screens = new List<Screen>();
            CreateDate = DateTime.UtcNow;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="portfolio"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        public Application(Portfolio portfolio, string description, ApplicationType type)
            : this()
        {
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

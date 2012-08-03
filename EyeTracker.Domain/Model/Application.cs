using System;
using System.Collections.Generic;
using EyeTracker.Common.Entities;

namespace EyeTracker.Domain.Model
{

    /// <summary>
    /// this class represents application data mode 
    /// instance of this class holds information about one single applications 
    /// and describes application properties 
    /// </summary>
    public class Application
    {
        private Iesi.Collections.Generic.ISet<Screen> screens;

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
        public virtual IEnumerable<Screen> Screens { get { return screens; } set { } }

        public Application()
        {
            this.screens = new Iesi.Collections.Generic.HashedSet<Screen>();
            this.CreateDate = DateTime.UtcNow;
        }

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

        public virtual void AddScreen(Screen screen)
        {
            this.screens.Add(screen);
        }

        public virtual void RemoveScreen(Screen screen)
        {
            if (this.screens.Contains(screen))
            {
                this.screens.Remove(screen);
            }
        }
    }
}

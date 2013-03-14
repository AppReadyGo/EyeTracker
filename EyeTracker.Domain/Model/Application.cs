using System;
using System.Collections.Generic;
using EyeTracker.Common.Entities;
using NHibernate.Collection.Generic;
using EyeTracker.Domain.Model.Users;

namespace EyeTracker.Domain.Model
{

    /// <summary>
    /// this class represents application data mode 
    /// instance of this class holds information about one single applications 
    /// and describes application properties 
    /// </summary>
    public class Application
    {
        private PersistentGenericSet<Screen> screens;

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
        /// this member holds User 
        /// </summary>
        public virtual User User { get; protected set; }
        /// <summary>
        /// holds application screens list 
        /// </summary>
        public virtual IEnumerable<Screen> Screens { get { return screens; } }

        public virtual Package Package { get; protected set; }

        public Application()
        {
            this.screens = new PersistentGenericSet<Screen>();
            this.CreateDate = DateTime.UtcNow;
        }

        public Application(User user, string description, ApplicationType type)
            : this()
        {
            this.Description = description;
            this.Type = type;
            this.User = user;
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

        public virtual void SetPakage(Package package)
        {
            this.Package = package;
        }
    }
}

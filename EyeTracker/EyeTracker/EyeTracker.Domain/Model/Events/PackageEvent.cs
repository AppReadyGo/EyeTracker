using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EyeTracker.Domain.Model.Events
{
    /// <summary>
    /// The class is just for serialisation
    /// </summary>
    [DataContract]
    public class PackageEvent : IEvent
    {
        public PackageEvent()
        {
            clicks = new List<ClickEvent>();
            parts = new List<ViewPartEvent>();
            Sessions = new List<SessionInfoEvent>();
        }
        /// <summary>
        /// CID - application ID 
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// client Screen Width
        /// </summary>
        public virtual int ScreenWidth { get; set; }
        /// <summary>
        /// client Screen Height
        /// </summary>
        public virtual int ScreenHeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<SessionInfoEvent> Sessions { get; set; }

        public List<ClickEvent> clicks { get; set; }
        public List<ViewPartEvent> parts { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using EyeTracker.Common.Interfaces;

namespace EyeTracker.Domain.Model.Events
{
    /// <summary>
    /// The class is just for serialisation
    /// </summary>
    [DataContract]
    public class PackageEvent : IPackageEvent
    {
        public PackageEvent()
        {
            //clicks = new List<ClickEvent>();
            //parts = new List<ViewPartEvent>();
            Sessions = new List<SessionInfoEvent>();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual long Id { get; set; }

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
        public virtual IList<SessionInfoEvent> Sessions { get; set; }

        //todo:remove
        //public List<ClickEvent> clicks { get; set; }
        //public List<ViewPartEvent> parts { get; set; }
    }
}
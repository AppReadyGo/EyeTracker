using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using EyeTracker.Common.Interfaces;
using EyeTracker.Common;

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
        /// client IP
        /// </summary>
        public virtual string Ip { get; set; }

        /// <summary>
        /// language
        /// </summary>
        public virtual Language Language { get; set; }

        /// <summary>
        /// client's Country
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// client's City
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// client's OS
        /// </summary>
        public virtual OperationSystem OperationSystem { get; set; }

        /// <summary>
        /// client's Browser
        /// </summary>
        public virtual Browser Browser { get; set; }

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

        /// <summary>
        /// client's Application - identified by Key property
        /// </summary>
        public virtual Application Application { get; set; }

        /// <summary>
        /// todo: client's device info
        /// </summary>
        public virtual IJsonSystemInfo SystemInfo { get; set; }

        /// <summary>
        /// todo: do we need it? we have Ip property
        /// </summary>
        //public virtual RequestInfo MyRequestInfo { get; set; }
    }
}
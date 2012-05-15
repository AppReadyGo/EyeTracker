using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Interfaces;

namespace EyeTracker.Domain.Model.Events
{

    /// <summary>
    /// represents one single using session data 
    /// </summary>
    public class SessionInfoEvent : IEvent
    {

        public SessionInfoEvent()
        {
            Clicks = new List<ClickEvent>();
            Scrolls = new List<ScrollEvent>();
            ScreenViewParts = new List<ViewPartEvent>();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int ClientWidth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int ClientHeight { get; set; }
        /// <summary>
        /// Start this session date
        /// </summary>
        public virtual DateTime StartDate { get; set; }
        /// <summary>
        /// Finish this session date
        /// </summary>
        public virtual DateTime CloseDate { get; set; }
        /// <summary>
        /// Click/Touches 
        /// </summary>
        public virtual IList<ClickEvent> Clicks { get; set; }
        /// <summary>
        /// Scrolls for this session
        /// </summary>
        public virtual IList<ScrollEvent> Scrolls { get; set; }
        /// <summary>
        /// Parts on the main view data 
        /// </summary>
        public virtual IList<ViewPartEvent> ScreenViewParts { get; set; }

        public virtual PackageEvent PackageEvent { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Events
{

    /// <summary>
    /// this class represents scrolling data
    /// </summary>
    public class ScrollEvent : IEvent
    {
        public virtual long Id { get; set; }

        /// <summary>
        /// Start scrolling property
        /// </summary>
        public virtual ClickEvent FirstTouch { get; set; }

        /// <summary>
        /// Finish scrolling property
        /// </summary>
        public virtual ClickEvent LastTouch { get; set; }

        public virtual SessionInfoEvent SessionInfoEvent { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Events
{
    public class ClickEvent : IEvent
    {
        public virtual long Id { get; set; }

        public virtual int ClientX { get; set; }

        public virtual int ClientY { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual long VisitInfoId { get; set; }

        public virtual int Press { get; set; }

        public virtual SessionInfoEvent SessionInfoEvent { get; set; }

        public virtual ScrollEvent ScrollEvent { get; set; }
    }
}

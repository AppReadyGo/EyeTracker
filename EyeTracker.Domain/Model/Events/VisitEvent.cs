using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Events
{
    public class VisitEvent : IEvent
    {
        public virtual long Id { get; set; }

        public virtual string Key { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string Path { get; set; }

        public virtual int PreviousVisitId { get; set; }

        public virtual string Ip { get; set; }

        public virtual string Language { get; set; }

        public virtual string OS { get; set; }

        public virtual string Browser { get; set; }

        public virtual int ScreenWidth { get; set; }

        public virtual int ScreenHeight { get; set; }

        public virtual int ClientWidth { get; set; }

        public virtual int ClientHeight { get; set; }

        public virtual IEnumerable<ClickEvent> Clicks { get; set; }

        public virtual IEnumerable<ViewPartEvent> ViewParts { get; set; }
    }
}

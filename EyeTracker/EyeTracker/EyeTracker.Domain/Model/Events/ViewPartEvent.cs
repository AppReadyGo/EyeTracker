using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Events
{
    public class ViewPartEvent
    {
        public virtual int ScrollTop { get; set; }

        public virtual int ScrollLeft { get; set; }

        public virtual long TimeSpan { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual long VisitInfoId { get; set; }
    }
}

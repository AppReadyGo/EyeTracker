using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Events
{
    public class ClickEvent
    {
        public int ClientX { get; set; }

        public int ClientY { get; set; }

        public DateTime Date { get; set; }

        public long VisitInfoId { get; set; }
    }
}

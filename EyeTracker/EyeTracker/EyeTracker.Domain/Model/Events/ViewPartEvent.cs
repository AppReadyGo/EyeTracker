using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Events
{
    public class ViewPartEvent
    {
        public int ScrollTop { get; set; }

        public int ScrollLeft { get; set; }

        public long TimeSpan { get; set; }

        public DateTime Date { get; set; }

        public long VisitInfoId { get; set; }
    }
}

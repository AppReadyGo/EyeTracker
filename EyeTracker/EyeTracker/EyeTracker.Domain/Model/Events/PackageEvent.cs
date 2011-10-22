using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Domain.Model.Events
{
    public class PackageEvent
    {
        public List<ClickEvent> clicks { get; set; }
        public List<ViewPartEvent> parts { get; set; }
    }
}
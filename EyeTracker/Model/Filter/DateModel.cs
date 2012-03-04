using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Filter
{
    public class DateModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime DateFromMin { get; set; }
        public DateTime DateFromMax { get; set; }
        public DateTime DateToMin { get; set; }
        public DateTime DateToMax { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EyeTracker.Core.Models
{
    [DataContract]
    public class ViewPartInfoViewModel
    {
        [DataMember(Name = "sd")]
        public string StrStartDate { get; set; }

        //public DateTime StartDate { get { return DateTime.Parse(StrStartDate); } }

        [DataMember(Name = "sl")]
        public int ScrollLeft { get; set; }

        [DataMember(Name = "st")]
        public int ScrollTop { get; set; }

        [DataMember(Name = "fd")]
        public string StrFinishDate { get; set; }

        //public DateTime FinishDate { get { return DateTime.Parse(StrFinishDate); } }
    }
}

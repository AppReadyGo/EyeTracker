using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EyeTracker.Core.Models
{
    [DataContract]
    public class ClickInfoViewModel
    {
        [DataMember(Name = "d")]
        public string StrDate { get; set; }

        [DataMember(Name = "cx")]
        public int ClientX { get; set; }

        [DataMember(Name = "cy")]
        public int ClientY { get; set; }
    }
}

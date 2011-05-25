using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EyeTracker.Core.Models
{
    [DataContract]
    public class VisitInfoViewModel
    {
        [DataMember(Name = "cid")]
        public string ClientId { get; set; }

        [DataMember(Name = "sw")]
        public int ScreenWidth { get; set; }

        [DataMember(Name = "sh")]
        public int ScreenHeight { get; set; }

        [DataMember(Name = "cw")]
        public int ClientWidth { get; set; }

        [DataMember(Name = "ch")]
        public int ClientHeight { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }
}

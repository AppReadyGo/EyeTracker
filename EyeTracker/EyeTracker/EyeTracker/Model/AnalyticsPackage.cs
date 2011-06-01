using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using EyeTracker.Core.Models;

namespace EyeTracker.Model
{
    [DataContract]
    public class AnalyticsPackage
    {
        [DataMember(Name = "vid")]
        public long VisitId { get; set; }

        [DataMember(Name = "vpd")]
        public ViewPartInfoViewModel[] ViewParts { get; set; }

        [DataMember(Name = "cd")]
        public ClickInfoViewModel[] Clicks { get; set; }
    }
}
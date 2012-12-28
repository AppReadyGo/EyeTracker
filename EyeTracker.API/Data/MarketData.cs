using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EyeTracker.API.Data
{
    [DataContract()]
    public class MarketData
    {

        [DataMember(Name="uid")]
        public string User { get; set; }

        [DataMember(Name="data")]
        public string Data { get; set; }
    }
}
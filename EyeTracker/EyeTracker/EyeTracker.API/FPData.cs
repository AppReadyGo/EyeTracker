using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EyeTracker.API
{

    [DataContract()]
    public class FPData
    {
        [DataMember(Name="val")]
        public System.String val 
        { 
            get; 
            set; 
        }
    }
}

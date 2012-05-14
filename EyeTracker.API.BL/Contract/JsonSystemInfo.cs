using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EyeTracker.API.BL.Contract
{
    /// <summary>
    /// Represents System information about a mobile device 
    ///si //system info
    ///{
    ///     brn     //string - brand name
    ///     den     //string - device name
    ///     din     //string - display name
    ///     fin     //string - fingerprint name (not our; this is android property)
    ///     han     //string - hardware name
    ///     man     //string - manufacturer name
    ///     mon     //string - model name
    ///     opn     //string - operator name
    ///     prn     //string - product name
    ///     con     //string - The current development codename, or the string "REL" if this is a release build.
    ///     inc     //string - The internal value used by the underlying source control to represent this build.
    ///     rel     //string - The user-visible version string.
    ///     sdki    //integer - The user-visible SDK version of the framework; its possible values are defined in         //Build.VERSION_CODES.
    ///}
    /// </summary>
    public class JsonSystemInfo
    {
        [DataMember(Name="brn")]
        public string BrandName { get; set; }

        [DataMember(Name = "den")]
        public string DeviceName { get; set; }

        [DataMember(Name = "din")]
        public string DisplayName { get; set; }

        [DataMember(Name = "fin")]
        public string FingerprintName { get; set; }

        [DataMember(Name = "han")]
        public string HardwareName { get; set; }

        [DataMember(Name = "man")]
        public string ManufactureName { get; set; }

        [DataMember(Name = "mon")]
        public string ModelName { get; set; }

        [DataMember(Name = "opn")]
        public string OperatorName { get; set; }

        //[DataMember(Name = "brn")]
        //public string BrandName { get; set; }

        [DataMember(Name = "skdi")]
        public string SdkIdentName { get; set; }

        [DataMember(Name = "rel")]
        public string RealVersionName { get; set; }

        [DataMember(Name = "inc")]
        public string InternalName { get; set; }

        [DataMember(Name = "con")]
        public string DevCodeName { get; set; }

        [DataMember(Name = "prn")]
        public string ProductName { get; set; }
    }
}

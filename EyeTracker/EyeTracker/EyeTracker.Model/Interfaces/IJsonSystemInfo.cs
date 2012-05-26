using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Interfaces
{
    /// <summary>
    /// Represents System information about a mobile device 
    ///ssi //system info
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
    ///     sdki    //integer - The user-visible SDK version of the framework; its possible values are defined in Build.VERSION_CODES.
    ///}
    /// </summary>
    public interface IJsonSystemInfo //: IPackage
    {
        string BrandName { get; set; }

        string DeviceName { get; set; }

        string DisplayName { get; set; }

        string FingerprintName { get; set; }

        string HardwareName { get; set; }

        string ManufactureName { get; set; }

        string ModelName { get; set; }

        string OperatorName { get; set; }

        string SdkIdentName { get; set; }

        string RealVersionName { get; set; }

        string InternalName { get; set; }
        
        string DevCodeName { get; set; }

        string ProductName { get; set; }
    }
}

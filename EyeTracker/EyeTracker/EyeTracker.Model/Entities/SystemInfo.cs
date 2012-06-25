using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Entities
{
    public class SystemInfo
    {
        public virtual string BrandName { get; protected set; }

        public virtual string DeviceName { get; protected set; }

        public virtual string DisplayName { get; protected set; }

        public virtual string FingerprintName { get; protected set; }

        public virtual string HardwareName { get; protected set; }

        public virtual string ManufactureName { get; protected set; }

        public virtual string ModelName { get; protected set; }

        public virtual string OperatorName { get; protected set; }

        public virtual string SdkIdentName { get; protected set; }

        public virtual string RealVersionName { get; protected set; }

        public virtual string InternalName { get; protected set; }

        public virtual string DevCodeName { get; protected set; }

        public virtual string ProductName { get; protected set; }

        public SystemInfo(string brandName,
            string deviceName,
            string displayName,
            string fingerprintName,
            string hardwareName,
            string manufactureName,
            string modelName,
            string operatorName,
            string sdkIdentName,
            string realVersionName,
            string internalName,
            string devCodeName,
            string productName)
        {
            this.DeviceName = deviceName;
            this.DisplayName = displayName;
            this.FingerprintName = fingerprintName;
            this.HardwareName = hardwareName;
            this.ManufactureName = manufactureName;
            this.ModelName = modelName;
            this.OperatorName = operatorName;
            this.SdkIdentName = sdkIdentName;
            this.RealVersionName = realVersionName;
            this.InternalName = internalName;
            this.DevCodeName = devCodeName;
            this.ProductName = productName;
        }

        public SystemInfo()
        {
        }
    }
}

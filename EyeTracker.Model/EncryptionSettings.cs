using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EyeTracker.Common
{
    public class EncryptionSettings : ConfigurationSection
    {
        private static EncryptionSettings settings = ConfigurationManager.GetSection("encryptionSettings") as EncryptionSettings;

        public static EncryptionSettings Settings
        {
            get { return settings; }
        }

        [ConfigurationProperty("passPhrase", IsRequired = true)]
        public string PassPhrase
        {
            get { return (string)this["passPhrase"]; }
            set { this["passPhrase"] = value; }
        }

        [ConfigurationProperty("initVector", IsRequired = true)]
        public string InitVector
        {
            get { return (string)this["initVector"]; }
            set { this["initVector"] = value; }
        }

        [ConfigurationProperty("saltVaue", IsRequired = true)]
        public string SaltVaue
        {
            get { return (string)this["saltVaue"]; }
            set { this["saltVaue"] = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Xml;

namespace EyeTracker.Common
{
    public class EmailSettings : ConfigurationSection
    {
        private static EmailSettings settings = ConfigurationManager.GetSection("emailSettings") as EmailSettings;

        public static EmailSettings Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("enabled", IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("forward", IsRequired = true)]
        public bool Forward
        {
            get { return (bool)this["forward"]; }
            set { this["forward"] = value; }
        }

        [ConfigurationProperty("smtp")]
        public SmtpElement Smtp
        {
            get{ return (SmtpElement)base["smtp"]; }
        }

        [ConfigurationProperty("email")]
        public EmailElement Email
        {
            get { return (EmailElement)base["email"]; }
        }
    }

    public class SmtpElement : ConfigurationElement
    {
        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return (string)this["host"]; }
            set { this["host"] = value; }
        }

        [ConfigurationProperty("userName", IsRequired = true)]
        public string UserName
        {
            get { return (string)this["userName"]; }
            set { this["userName"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }

        [ConfigurationProperty("enableSsl", IsRequired = true)]
        public bool EnableSsl
        {
            get { return (bool)this["enableSsl"]; }
            set { this["enableSsl"] = value; }
        }
    }

    public class EmailElement : ConfigurationElement
    {
        [ConfigurationProperty("from", IsRequired = false)]
        public string From
        {
            get { return (string)this["from"]; }
            set { this["from"] = value; }
        }

        [ConfigurationProperty("forward", IsRequired = false)]
        public string Forward
        {
            get { return (string)this["forward"]; }
            set { this["forward"] = value; }
        }

        [ConfigurationProperty("fromName", IsRequired = false)]
        public string FromName
        {
            get { return (string)this["fromName"]; }
            set { this["fromName"] = value; }
        }
        
    }
}
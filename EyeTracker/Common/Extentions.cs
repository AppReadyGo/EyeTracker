using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Common.Entities;
using System.Configuration;

namespace EyeTracker.Common
{
    public static class Extentions
    {
        public static string GetContent(this ContentPredefinedKeys key)
        {
            switch (key)
            {
                case ContentPredefinedKeys.AndroidPackageVersion:
                    return ConfigurationManager.AppSettings["AndroidPackageVersion"];
                case ContentPredefinedKeys.ContentVersion:
                    return ConfigurationManager.AppSettings["ContentVersion"];
                default:
                    return string.Empty;
            }
        }

        public static string GetAppKey(this ApplicationType type, int applicationId)
        {
            string key = "";
            switch (type)
            {
                case ApplicationType.Android:
                    key = "MA";
                    break;
                case ApplicationType.Web:
                    key = "WP";
                    break;
                case ApplicationType.iPhone:
                    key = "MI";
                    break;
                case ApplicationType.WebMobile:
                    key = "WM";
                    break;
                case ApplicationType.WindowsMobile:
                    key = "MW";
                    break;
            }

            return string.Format("{0}-{1:000000}", key, applicationId);
        }

        //public static string GetContentUrl(this Url
    }
}
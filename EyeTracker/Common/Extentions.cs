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

        //public static string GetContentUrl(this Url
    }
}
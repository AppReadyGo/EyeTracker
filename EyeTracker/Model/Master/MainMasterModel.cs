using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace EyeTracker.Model.Master
{
    public class MainMasterModel
    {
        public bool GoogleAnalytics { get; private set; }

        public MainMasterModel()
        {
            this.GoogleAnalytics = bool.Parse(ConfigurationManager.AppSettings["GoogleAnalytics"]);
        }
    }
}
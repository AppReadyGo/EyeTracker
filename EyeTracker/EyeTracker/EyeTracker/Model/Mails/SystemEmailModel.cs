using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Mails
{
    public class SystemEmailModel : MailMasterModel
    {
        public string SiteRootUrl { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ContactUsEmail { get; set; }

        public SystemEmailModel(bool isEmailProcess)
            : base(isEmailProcess)
        {
        }
    }

}
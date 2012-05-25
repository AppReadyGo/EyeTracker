using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Mails
{
    public class MailMasterModel
    {
        public bool IsEmailProcess { get; set; }

        public MailMasterModel(bool isEmailProcess)
        {
            this.IsEmailProcess = isEmailProcess;
        }
    }
}
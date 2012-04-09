using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Pages.Home.Mails
{
    public class BasicMailContentModel : MailContentModel
    {
        public string Subject { get; set; }

        public string ThisEmailUrl { get; set; }

        public string SiteRootUrl { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public bool IsMail { get; set; }

        public MailTemplate Template { get; set; }

        public enum MailTemplate
        {
            Basic
        }
    }
}
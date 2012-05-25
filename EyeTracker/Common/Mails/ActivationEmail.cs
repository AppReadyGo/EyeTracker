using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Common.Queries;
using EyeTracker.Common;
using EyeTracker.Core;
using EyeTracker.Model.Mails;

namespace EyeTracker.Common.Mails
{
    public class ActivationEmail : SystemEmail
    {
        public ActivationEmail(string email)
        {
            this.To = new string[] { email };
            string contentPath = string.Format("mails/{0}", this.GetType().Name);
            var content = ObjectContainer.Instance.RunQuery(new GetKeyContentQuery(contentPath.ToLower()));

            string activationKey = string.Format("{0},{1}", DateTime.Now.AddDays(EmailSettings.Settings.LinksExpire.Activation), email).EncryptLow();
            string siteRootUrl = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
            string activationLnk = string.Format("{0}/Account/Activate?key={1}", siteRootUrl, HttpUtility.UrlEncode(activationKey));
            string body = content["body"].Replace("{activation_link}", activationLnk);
            string subject = content["subject"];

            this.Model = new SystemEmailModel(true)
            {
                ContactUsEmail = EmailSettings.Settings.Email.ContactUsEmail,
                SiteRootUrl = siteRootUrl,
                Subject = subject,
                Body = body
            };

            this.Subject = subject;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Core;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Common.Queries;
using EyeTracker.Model.Mails;

namespace EyeTracker.Common.Mails
{
    public class PromotionEmail : Email
    {
        public PromotionEmail(string emailKey, UserDetailsResult userDetails)
            : base("~/Views/Mails/Promotion.aspx")
        {
            this.To = new string[] { userDetails.Email };
            string contentPath = string.Format("mails/{0}", emailKey);
            var content = ObjectContainer.Instance.RunQuery(new GetKeyContentQuery(contentPath.ToLower()));

            string siteRootUrl = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
            string userName = string.IsNullOrEmpty(userDetails.FirstName) || string.IsNullOrEmpty(userDetails.LastName) ? userDetails.Email : string.Join(" ", userDetails.FirstName, userDetails.LastName);
            string body = content["Body"].Replace("{user_name}", userName);
            string subject = content["subject"];

            this.Model = new PromotionEmailModel(true)
            {
                ContactUsEmail = EmailSettings.Settings.Email.ContactUsEmail,
                SiteRootUrl = siteRootUrl,
                Subject = subject,
                Body = body,
                ThisEmailUrl = string.Format("{0}/mails/{1}", siteRootUrl, emailKey),
                UnsubscribeUrl = string.Format("{0}/Account/Unsubscribe/{1}", siteRootUrl, userDetails.Email)
            };

            this.Subject = subject;
        }
    }
}
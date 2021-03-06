﻿using System;
using System.Web;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Core;
using EyeTracker.Model.Mails;

namespace EyeTracker.Common.Mails
{
    public class ForgotPasswordMail : SystemEmail
    {
        public ForgotPasswordMail(string email)
        {
            this.To = new string[] { email };
            var mailContent = GetMailContent();

            string activationKey = string.Format("{0},{1}", DateTime.Now.AddDays(EmailSettings.Settings.LinksExpire.ForgotPassword), email).EncryptLow();
            string siteRootUrl = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
            string activationLnk = string.Format("{0}/Account/ResetPassword/?key={1}", siteRootUrl, HttpUtility.UrlEncode(activationKey));
            string body = mailContent.Body.Replace("{reset_password_link}", activationLnk);

            this.Model = new SystemEmailModel(true)
            {
                ContactUsEmail = EmailSettings.Settings.Email.ContactUsEmail,
                SiteRootUrl = siteRootUrl,
                Subject = mailContent.Subject,
                Body = body
            };

            this.Subject = mailContent.Subject;
        }
    }
}
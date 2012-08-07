using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Net.Mail;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using EyeTracker.Common.Logger;


namespace EyeTracker.Common
{
    public class Messenger
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public static void SendEmail(IEnumerable<string> to, string subject, string body, IEnumerable<string> cc = null, IEnumerable<string> bcc = null)
        {
            SendEmail(null, to, subject, body, cc, bcc);
        }

        public static void SendEmail(string from, IEnumerable<string> to, string subject, string body, IEnumerable<string> cc = null, IEnumerable<string> bcc = null)
        {
            try
            {
                string fromAccount = string.IsNullOrEmpty(from) ? (string.IsNullOrEmpty(EmailSettings.Settings.Email.From) ? EmailSettings.Settings.Smtp.UserName : EmailSettings.Settings.Email.From) : from;
                if (!EmailSettings.Settings.Enabled)
                {
                    log.WriteVerbose("The email was not sended, Messenger is disabled. From:{0} To:{1} Subject:{2}, Body:{3}", fromAccount, string.Join(";", to.ToArray()), subject, body);
                    return;
                }

                MailMessage mail = new MailMessage();
                if (EmailSettings.Settings.Forward && !string.IsNullOrEmpty(EmailSettings.Settings.Email.Forward))
                {
                    mail.To.Add(EmailSettings.Settings.Email.Forward);
                    body = string.Format("The email originally sent to: <br>{0} <br> Body: <br>{1}", string.Join(";", to.ToArray()), body);
                }
                else
                {
                    foreach (string curTo in to)
                    {
                        mail.To.Add(curTo);
                    }
                }

                if (string.IsNullOrEmpty(EmailSettings.Settings.Email.FromName))
                {
                    mail.From = new MailAddress(fromAccount);
                }
                else
                {
                    mail.From = new MailAddress(fromAccount, EmailSettings.Settings.Email.FromName);
                }

                mail.Subject = subject;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                //<b>Welcome to codedigest.com!!</b>
                //<br><BR>Online resource for .net articles.<BR>
                //<img alt=\"\" hspace=0 src=\"{#}imgageId|imagePath{#}\" align=baseline border=0 >

                //TODO: replace {#}imgageId|imagePath{#} to cid:imageId in body
                //Get from body images urls
                //var imagesPathes = new Dictionary<string, string>();
                //foreach (var item in imagesPathes)
                //{
                //    int idx = item.Value.LastIndexOf('.');
                //    string extention = item.Value.Substring(idx + 1);
                //    string imageType = string.Empty;
                //    if (extention.Equals("png", StringComparison.OrdinalIgnoreCase))
                //        imageType = "png";
                //    else if (extention.Equals("jpg", StringComparison.OrdinalIgnoreCase))
                //        imageType = "jpeg";
                //    else if (extention.Equals("tiff", StringComparison.OrdinalIgnoreCase))
                //        imageType = "tiff";

                //    LinkedResource imagelink = new LinkedResource(HttpContext.Current.Server.MapPath(".") + item.Value, "image/" + imageType);
                //    imagelink.ContentId = item.Key;
                //    imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                //    htmlView.LinkedResources.Add(imagelink);
                //}
                mail.AlternateViews.Add(htmlView);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = EmailSettings.Settings.Smtp.Host;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;// PickupDirectoryFromIis;
                smtp.Credentials = new System.Net.NetworkCredential(EmailSettings.Settings.Smtp.UserName, EmailSettings.Settings.Smtp.Password);
                smtp.EnableSsl = EmailSettings.Settings.Smtp.EnableSsl;
                smtp.Port = EmailSettings.Settings.Smtp.Port;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                log.WriteError(false, ex, "Error send email: {0}, Subject:{1}, Body:{2}", to, subject, body);
            }
        }
    }
}

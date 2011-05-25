using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Net.Mail;
using CirriNet.Common;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CirriNet.Logger;
using CirriNet.DAL;
using System.Web;
using EyeTracker.Common.Logger;


namespace EyeTracker.Common
{
    public class Messenger
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);


        private static void SendEmail(string from, List<string> toList, string subject, string body)
        {
            try
            {
                string smtpAccount = ConfigurationManager.AppSettings["smtpAccount"];
                string smtpAccountFrom = ConfigurationManager.AppSettings["smtpAccountFrom"];
                string fromAccount = string.IsNullOrEmpty(from) ? (string.IsNullOrEmpty(smtpAccountFrom) ? smtpAccount : smtpAccountFrom) : from;
                bool isEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["isMessengerEnabled"]);
                if (!isEnabled)
                {
                    log.WriteVerbose("The email was not sended, Messenger is disabled. From:{0} To:{1} Subject:{2}, Body:{3}", fromAccount, string.Join(";", toList.ToArray()), subject, body);
                    return;
                }

                MailMessage mail = new MailMessage();
                foreach (string curTo in toList)
                {
                    mail.To.Add(curTo);
                }

                mail.From = new MailAddress(fromAccount);

                mail.Subject = subject;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                //<b>Welcome to codedigest.com!!</b>
                //<br><BR>Online resource for .net articles.<BR>
                //<img alt=\"\" hspace=0 src=\"{#}imgageId|imagePath{#}\" align=baseline border=0 >

                //TODO: replace {#}imgageId|imagePath{#} to cid:imageId in body
                //Get from body images urls
                var imagesPathes = new Dictionary<string, string>();
                foreach (var item in imagesPathes)
                {
                    int idx = item.Value.LastIndexOf('.');
                    string extention = item.Value.Substring(idx + 1);
                    string imageType = string.Empty;
                    if (extention.Equals("png", StringComparison.OrdinalIgnoreCase))
                        imageType = "png";
                    else if (extention.Equals("jpg", StringComparison.OrdinalIgnoreCase))
                        imageType = "jpeg";
                    else if (extention.Equals("tiff", StringComparison.OrdinalIgnoreCase))
                        imageType = "tiff";

                    LinkedResource imagelink = new LinkedResource(HttpContext.Current.Server.MapPath(".") + item.Value, "image/" + imageType);
                    imagelink.ContentId = item.Key;
                    imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    htmlView.LinkedResources.Add(imagelink);
                }
                mail.AlternateViews.Add(htmlView);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["smtpServer"];
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;// PickupDirectoryFromIis;
                smtp.Credentials = new System.Net.NetworkCredential(smtpAccount, ConfigurationManager.AppSettings["smtpPassword"]);
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                log.WriteError(false, ex, "Error send email: {0}, Subject:{1}, Body:{2}", toList, subject, body);
            }
        }
    }
}

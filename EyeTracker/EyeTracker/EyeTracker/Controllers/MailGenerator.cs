using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using EyeTracker.Model.Pages.Home.Mails;
using EyeTracker.Common;
using EyeTracker.Core;
using EyeTracker.Common.Queries;

namespace EyeTracker.Controllers
{
    public class MailGenerator
    {
        public ControllerContext ControllerContext { get; private set; }
        private ViewDataDictionary ViewData;
        private TempDataDictionary TempData;
        public BasicMailContentModel Model{ get; private set; }

        public MailGenerator(ControllerContext controllerContext, string urlPath1, string urlPath2, string urlPath3)
        {
            /*
            var view = (ViewResult)new HomeController().MailContent(urlPath1, urlPath2, urlPath3, true);
            this.ViewData = view.ViewData;
            this.TempData = view.TempData;
            this.Model = ((BasicMailContentModel)view.ViewData.Model);
            */
            this.Model = this.GetMailModel(controllerContext, urlPath1, urlPath2, urlPath3);
            this.ViewData = new ViewDataDictionary(this.Model);
            this.TempData = new TempDataDictionary();
            this.ControllerContext = controllerContext;
        }

        private string RenderViewToString(ControllerContext controllerContext, string viewPath)
        {
            ViewPage viewPage = new ViewPage();
            StringWriter strWriter = new StringWriter();

            //Right, create our view
            viewPage.ViewContext = new ViewContext(controllerContext, new WebFormView(controllerContext, viewPath), this.ViewData, this.TempData, strWriter);

            //Now render the view into the memorystream and flush the response
            viewPage.ViewContext.View.Render(viewPage.ViewContext, viewPage.ViewContext.HttpContext.Response.Output);

            return strWriter.ToString();
        }

        public void Send(IEnumerable<string> toEmails)
        {
            string subject = this.Model.Subject;
            string body = RenderViewToString(this.ControllerContext, string.Format("~/Views/Home/Mails/{0}.aspx", this.Model.Template));

            Messenger.SendEmail("noreply@mobillify.com", toEmails, subject, body);
        }

        private BasicMailContentModel GetMailModel(ControllerContext controllerContext, string urlPath1, string urlPath2, string urlPath3, bool isMail = false)
        {
            string path = "mails/";
            BasicMailContentModel.MailTemplate template = BasicMailContentModel.MailTemplate.Basic;
            if (!Enum.TryParse<BasicMailContentModel.MailTemplate>(urlPath1, true, out template))
            {
                template = BasicMailContentModel.MailTemplate.Basic;
                path += urlPath1;
            }

            if (!string.IsNullOrEmpty(urlPath2))
            {
                path += "/" + urlPath2;
            }
            if (!string.IsNullOrEmpty(urlPath3))
            {
                path += "/" + urlPath3;
            }

            BasicMailContentModel model = null;
            var keys = ObjectContainer.Instance.RunQuery(new GetKeyContent(path.ToLower()));
            if (keys.Any())
            {
                string rootUrl = string.Format("{0}://{1}", controllerContext.HttpContext.Request.Url.Scheme, controllerContext.HttpContext.Request.Url.Authority);
                switch (template)
                {
                    case BasicMailContentModel.MailTemplate.Basic:
                        model = new BasicMailContentModel 
                        { 
                            Template = template, 
                            Email = "support@mobillify.com", 
                            ThisEmailUrl = string.Format("{0}/{1}", rootUrl, path), 
                            Content = keys["body"], 
                            SiteRootUrl = rootUrl, 
                            Subject = keys["subject"],
                            Facebook = "http://www.facebook.com/mobillify",
                            Twitter = "http://twitter.com/mobillify",
                            IsMail = isMail 
                        };
                        break;
                }
            }

            return model;
        }
    }
}
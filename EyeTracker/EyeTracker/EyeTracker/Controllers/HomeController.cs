using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EyeTracker.Core;
using AutoMapper;
using EyeTracker.DAL.Domain;
using System.Web.Security;
using EyeTracker.Core.Services;
using EyeTracker.Domain.Model;
using EyeTracker.Model.Pages.Home;
using EyeTracker.Model.Master;
using EyeTracker.Common.Queries;
using EyeTracker.Model.Pages.Home.Mails;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Common.Mails;

namespace EyeTracker.Controllers
{
    [HandleError]
    public class HomeController : Master.BeforeLoginController
    {
        public HomeController()
        {
        }

        public ActionResult Index(long? appId, string pageUri, string clientSize)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("", "Analytics", new { });
            }
            else
            {
                return View(new IndexModel { }, BeforeLoginMasterModel.MenuItem.Home);
            }
        }

        public ActionResult Pricing()
        {
            return View(new PricingModel { }, BeforeLoginMasterModel.MenuItem.PlanAndPricing);
        }

        public ActionResult PlayGround()
        {
            return View(new PricingModel { }, BeforeLoginMasterModel.MenuItem.PlanAndPricing);
        }

        public ActionResult MailContent(string urlPart1, string urlPart2, string urlPart3)
        {
            /*
            string path = "mails/";
            BasicMailContentModel.MailTemplate template = BasicMailContentModel.MailTemplate.Basic;
            if (!Enum.TryParse<BasicMailContentModel.MailTemplate>(urlPart1, true, out template))
            {
                template = BasicMailContentModel.MailTemplate.Basic;
                path += urlPart1;
            }

            if (!string.IsNullOrEmpty(urlPart2))
            {
                path += "/" + urlPart2;
            }
            if (!string.IsNullOrEmpty(urlPart3))
            {
                path += "/" + urlPart3;
            }

            bool isMailGenerator = isMail.HasValue && isMail.Value;

            var keys = ObjectContainer.Instance.RunQuery(new GetKeyContent(path.ToLower()));
            if (!keys.Any())
            {
                return View("404", new PricingModel { }, BeforeLoginMasterModel.MenuItem.None);
            }
            else
            {
                string rootUrl = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                MailContentModel model = null;
                switch (template)
                {
                    case BasicMailContentModel.MailTemplate.Basic:
                        model = new BasicMailContentModel { Template = template, Email = "support@mobillify.com", ThisEmailUrl = string.Format("{0}/{1}", rootUrl, path), Content = keys["body"], SiteRootUrl = rootUrl, Subject = keys["subject"], Facebook = "", Twitter = "", IsMail = isMailGenerator };
                        break;
                }
                return View("Mails/" + template.ToString(), model);
            }
            */
            try
            {
                var email = new PromotionEmail(urlPart1, urlPart2, false);
                return View(email.EmailPagePath, email.Model);
            }
            catch
            {
                return View("404", new PricingModel { }, BeforeLoginMasterModel.MenuItem.None);
            }
        }

        public ActionResult PageContent(string urlPart1, string urlPart2, string urlPart3)
        {
            string path = urlPart1;
            if (!string.IsNullOrEmpty(urlPart2))
            {
                path += "/" + urlPart2;
            }
            if (!string.IsNullOrEmpty(urlPart3))
            {
                path += "/" + urlPart3;
            }

            var page = ObjectContainer.Instance.RunQuery(new GetPageQuery(path.ToLower()));
            if (page == null)
            {
                return View("404", new PricingModel { }, BeforeLoginMasterModel.MenuItem.None);
            }
            else
            {
                BeforeLoginMasterModel.MenuItem selectedItem = BeforeLoginMasterModel.MenuItem.None;
                if (!Enum.TryParse<BeforeLoginMasterModel.MenuItem>(urlPart1, true, out selectedItem))
                {
                    selectedItem = BeforeLoginMasterModel.MenuItem.None;
                }
                return View(new ContentModel { Title = page.Title, Content = page.Content }, selectedItem);
            }
        }
    }
}

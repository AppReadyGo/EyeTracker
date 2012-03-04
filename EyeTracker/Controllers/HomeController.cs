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
                return View(new IndexModel { }, BeforeLoginViewModel.SelectedMenuItem.Home);
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

            var keys = ObjectContainer.Instance.RunQuery(new GetKeyContent(path));
            if (!keys.Any())
            {
                return View("404");
            }
            else
            {
                var selectedItem = (BeforeLoginViewModel.SelectedMenuItem)Enum.Parse(typeof(BeforeLoginViewModel.SelectedMenuItem), urlPart1, true);
                return View(new ContentModel { Title = keys["Title"], Content = keys["Content"] }, selectedItem);
            }
        }
    }
}

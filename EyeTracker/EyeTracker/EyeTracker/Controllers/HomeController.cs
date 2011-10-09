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

namespace EyeTracker.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        IAnalyticsService service;
        public HomeController()
            : this( new AnalyticsService())
        {
        }

        public HomeController(IAnalyticsService service)
        {
            this.service = service;
        }

        public ActionResult Index(long? appId, string pageUri, string clientSize)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("", "Application", new { });
            }
            else
            {
                return View();
            }
        }

        public ActionResult PageContent(string urlPart1, string urlPart2, string urlPart3)
        {
            ViewBag.Url = urlPart1 + "/" + urlPart2 + "/" + urlPart3;
            return View();
        }
    }
}

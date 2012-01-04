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
                return RedirectToAction("", "Analytics", new { });
            }
            else
            {
                ViewData["home"] = "class=\"selected\"";
                return View();
            }
        }

        public ActionResult PageContent(string urlPart1, string urlPart2, string urlPart3)
        {
            ViewBag.Title = "Some content title";
            ViewBag.Content = "Some content";
            ViewBag.Url = urlPart1;
            if (!string.IsNullOrEmpty(urlPart2))
            {
                ViewBag.Url += "/" + urlPart2;
            }
            if (!string.IsNullOrEmpty(urlPart3))
            {
                ViewBag.Url += "/" + urlPart3;
            }
            ViewData[urlPart1] = "class=\"selected\"";
            return View();
        }
    }
}

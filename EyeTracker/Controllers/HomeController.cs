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
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

namespace EyeTracker.Controllers
{
    [HandleError]
    public class HomeController : Master.BeforeLoginController
    {
        public ActionResult Index(long? appId, string pageUri, string clientSize)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("", "Portfolio", new { });
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
    }
}

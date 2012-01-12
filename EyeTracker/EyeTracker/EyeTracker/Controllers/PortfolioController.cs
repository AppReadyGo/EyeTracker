using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Core.Services;
using EyeTracker.Helpers;
using EyeTracker.Model;
using EyeTracker.Common;
using System.Collections.ObjectModel;

namespace EyeTracker.Controllers
{
    public class PortfolioController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IPortfolioService portfolioService;

        public PortfolioController()
            : this(new PortfolioService())
        {
        }

        public PortfolioController(IPortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }

        public ActionResult New()
        {
            ViewData["analytics"] = "class=\"selected\"";
           
            var countriesRes = portfolioService.GetCountries();
            if (!countriesRes.HasError)
            {
                ViewData["TimeZoneList"] = this.GetTimeZones().Value.Select(curItem => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours });
                return View(new PortfolioModel());
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult New(PortfolioModel model)
        {
            ViewBag.Title = "New";
            ViewData["analytics"] = "class=\"selected\"";
            if (ModelState.IsValid)
            {
                var res = portfolioService.AddPortfolio(model.Description, model.TimeZone);
                if (res.HasError)
                {
                    return View("Error");
                }
                else
                {
                    return Redirect("/Analytics");
                }
            }
            else
            {
                var countriesRes = portfolioService.GetCountries();
                if (!countriesRes.HasError)
                {
                    ViewData["TimeZoneList"] = this.GetTimeZones().Value.Select(curItem => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours });
                    return View(new PortfolioModel());
                }
                else
                {
                    return View("Error");
                }
            }
        }

        public ActionResult Edit(int id)
        {/*
            var portfolioRes = portfolioService.Get(id);
            if (portfolioRes.HasError)
            {
                return View("Error");
            }
            else
            {
                var model = new PortfolioModel
                {
                    Id = portfolioRes.Value.Id,
                    Description = portfolioRes.Value.Description,
                    TimeZone = portfolioRes.Value.TimeZone
                };
                ViewBag.Title = "Edit";
                var countriesRes = portfolioService.GetCountries();
                if (!countriesRes.HasError)
                {
                    ViewData["TimeZoneList"] = this.GetTimeZones().Value.Select(curItem => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours });
                    return View("NewEdit", model);
                }
                else
                {
                    return View("Error");
                }
            }*/
            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(PortfolioModel model)
        {
            /*
            ViewBag.Title = "Edit";
            if (ModelState.IsValid)
            {
                var res = portfolioService.Update(model.Id, model.Description, model.TimeZone);
                return RedirectToAction("");
            }
            else
            {
                return View("NewEdit");
            }*/
            return View("Error");
        }

        public ActionResult Remove(int id)
        {
            /*
            var res = portfolioService.Remove(id);
            if (res.HasError)
            {
                return View("Error");
            }
            else
            {
                return RedirectToAction("");
            }
             */
             return View("Error");
        }


        public OperationResult<ReadOnlyCollection<TimeZoneInfo>> GetTimeZones()
        {
            return new OperationResult<ReadOnlyCollection<TimeZoneInfo>>(TimeZoneInfo.GetSystemTimeZones());
        }
    }
}

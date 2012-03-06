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
using EyeTracker.Controllers.Master;
using EyeTracker.Model.Pages.Portfolio;
using EyeTracker.Model.Master;

namespace EyeTracker.Controllers
{
    public class PortfolioController : Master.AnalyticsMasterController
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
            var countriesRes = portfolioService.GetCountries();
            if (!countriesRes.HasError)
            {
                var timeZones = this.GetTimeZones().Value.Select((curItem, i) => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours, i = i });
                return View(0,
                    new PortfolioModel { TimeZone = 0, ViewData = new SelectList(timeZones, "Id", "DisplayName") }, 
                    AnalyticsMasterModel.MenuItem.Portfolios, 
                    AfterLoginMasterModel.SelectedMenuItem.Analytics);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult New(PortfolioModel model)
        {
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
                    var timeZones = this.GetTimeZones().Value.Select((curItem, i) => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours, i = i });
                    model.ViewData = new SelectList(timeZones, "Id", "DisplayName");
                    return View(0,
                        model,
                        AnalyticsMasterModel.MenuItem.Portfolios,
                        AfterLoginMasterModel.SelectedMenuItem.Analytics);
                }
                else
                {
                    return View("Error");
                }
            }
        }

        public ActionResult Edit(int id)
        {
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
                var countriesRes = portfolioService.GetCountries();
                if (!countriesRes.HasError)
                {
                    var timeZones = this.GetTimeZones().Value.Select((curItem, i) => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours, i = i });
                    model.ViewData = new SelectList(timeZones, "Id", "DisplayName");
                    return View(0,
                        model,
                        AnalyticsMasterModel.MenuItem.Portfolios,
                        AfterLoginMasterModel.SelectedMenuItem.Analytics);
                }
                else
                {
                    return View("Error");
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(PortfolioModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var res = portfolioService.Update(viewModel.Id, viewModel.Description, viewModel.TimeZone);
                return Redirect("/Analytics");
            }
            else
            {
                return View(0,
                    viewModel,
                    AnalyticsMasterModel.MenuItem.Portfolios,
                    AfterLoginMasterModel.SelectedMenuItem.Analytics);
            }
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

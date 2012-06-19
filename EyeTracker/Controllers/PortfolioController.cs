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
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using EyeTracker.Model.Pages.Analytics;

namespace EyeTracker.Controllers
{
    [Authorize]
    public class PortfolioController : FilterController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IPortfolioService portfolioService;

        public PortfolioController()
            : this(new PortfolioService())
        {
            log.WriteInformation("");
        }

        public PortfolioController(IPortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }

        public override AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.MenuItem.Analytics; }
        }

        public ActionResult New()
        {
            var model = this.GetModel();
            model.TimeZone = 0;
            return View(model, AnalyticsMasterModel.MenuItem.Portfolios);
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
                return View(this.GetModel(model), AnalyticsMasterModel.MenuItem.Portfolios);
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
                var model = this.GetModel();
                model.Id = portfolioRes.Value.Id;
                model.Description = portfolioRes.Value.Description;
                model.TimeZone = portfolioRes.Value.TimeZone;
                
                return View(model, AnalyticsMasterModel.MenuItem.Portfolios);
            }
        }

        [HttpPost]
        public ActionResult Edit(PortfolioModel model)
        {
            if (ModelState.IsValid)
            {
                var res = portfolioService.Update(model.Id, model.Description, model.TimeZone);
                return Redirect("/Analytics");
            }
            else
            {
                return View(this.GetModel(model), AnalyticsMasterModel.MenuItem.Portfolios);
            }
        }

        public ActionResult Remove(int id)
        {
            var res = portfolioService.Remove(id);
            if (res.HasError)
            {
                return View("Error");
            }
            else
            {
                return RedirectToAction("", "Analytics");
            }
        }

        private PortfolioModel GetModel(PortfolioModel model = null)
        {
            var m = model == null ? new PortfolioModel() : model;

            var timeZones = TimeZoneInfo.GetSystemTimeZones().Select((curItem, i) => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours, i = i });
            m.ViewData = new SelectList(timeZones, "Id", "DisplayName");

            return m;
        }
    }
}

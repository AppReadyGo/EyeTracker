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
            : this(new PortfolioService(), new ApplicationService(), new AnalyticsService(), new AccountService(), new ReportsService())
        {
        }

        public PortfolioController(IPortfolioService portfolioService, IApplicationService applicationService, IAnalyticsService analyticsService, IAccountService accountService, IReportsService reportService)
        {
            this.portfolioService = portfolioService;
        }

        public ActionResult Index()
        {
            var res = portfolioService.GetAll();
            if (res.HasError)
            {
                return View("Error");
            }

            var columnHeaders = new List<HTMLTable.Cell>() {
                    new HTMLTable.Cell() { Value = "Description" }, 
                    new HTMLTable.Cell() { Value = "Applications" }, 
                    new HTMLTable.Cell() { Value = "% Change" }
                };
            var data = new List<List<HTMLTable.Cell>>();

            if (res.Value.Count > 0)
            {
                //Create table
                foreach (var curPortfolio in res.Value)
                {
                    var cells = new List<HTMLTable.Cell>();
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"/Portfolio/Remove/{0}\">remove</a>&nbsp;<a href=\"/Portfolio/Edit/{0}\">edit</a>&nbsp;<a href=\"/Analytics/Portfolio/Dashboard/{0}\">{1}</a>", curPortfolio.Id, curPortfolio.Description) });
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"/Application/{0}\" >{1}</a>", curPortfolio.Id, curPortfolio.Applications.Count()) });
                    cells.Add(new HTMLTable.Cell() { Value = "0.00%" });
                    data.Add(cells);
                }
            }
            else
            {
                data.Add(new List<HTMLTable.Cell>() { new HTMLTable.Cell() { ColSpan = 8, StyleClass = "no-data", Value = "No Portfolios" } });
            }

            ViewData["caption"] = new HTMLTable.Cell() { Value = "Accounts" };
            ViewData["columnHeaders"] = columnHeaders;
            ViewData["data"] = data;
            ViewData["Portfolios"] = "class=\"selected\"";
            return View();
        }

        public ActionResult New()
        {
            ViewBag.Title = "New";
            var countriesRes = portfolioService.GetCountries();
            if (!countriesRes.HasError)
            {
                ViewData["TimeZoneList"] = this.GetTimeZones().Value.Select(curItem => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours });
                return View("NewEdit", new PortfolioModel());
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
            if (ModelState.IsValid)
            {
                var res = portfolioService.AddPortfolio(model.Description, model.TimeZone);
                if (res.HasError)
                {
                    return View("Error");
                }
                else
                {
                    return RedirectToAction("");
                }
            }
            else
            {
                var countriesRes = portfolioService.GetCountries();
                if (!countriesRes.HasError)
                {
                    ViewData["TimeZoneList"] = this.GetTimeZones().Value.Select(curItem => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours });
                    return View("NewEdit", new PortfolioModel());
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
            }
        }

        [HttpPost]
        public ActionResult Edit(PortfolioModel model)
        {
            ViewBag.Title = "Edit";
            if (ModelState.IsValid)
            {
                var res = portfolioService.Update(model.Id, model.Description, model.TimeZone);
                return RedirectToAction("");
            }
            else
            {
                return View("NewEdit");
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
                return RedirectToAction("");
            }
        }


        public OperationResult<ReadOnlyCollection<TimeZoneInfo>> GetTimeZones()
        {
            return new OperationResult<ReadOnlyCollection<TimeZoneInfo>>(TimeZoneInfo.GetSystemTimeZones());
        }
    }
}

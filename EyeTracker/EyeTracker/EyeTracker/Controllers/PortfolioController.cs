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
using EyeTracker.Domain.Model;
using System.Web.Script.Serialization;
using EyeTracker.Core;
using EyeTracker.Common;
using EyeTracker.Domain;
using System.Collections.ObjectModel;

namespace EyeTracker.Controllers
{
    [Authorize]
    public class PortfolioController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IPortfolioService service;
        private IAccountService accountService;
        private IReportsService reportService;

        public PortfolioController()
            : this(new PortfolioService(), new AccountService(), new ReportsService())
        {
        }

        public PortfolioController(IPortfolioService service, IAccountService accountService, IReportsService reportService)
        {
            this.service = service;
            this.accountService = accountService;
            this.reportService = reportService;
        }

        public ActionResult Index()
        {
            var portfRes = service.GetAll();
            if (portfRes.HasError)
            {
                return View("Error");
            }

            var columnHeaders = new List<HTMLTable.Cell>() {
                    new HTMLTable.Cell() { Value = "Description" }, 
                    new HTMLTable.Cell() { Value = "Applications" }, 
                    new HTMLTable.Cell() { Value = "% Change" }
                };
            var data = new List<List<HTMLTable.Cell>>();

            if (portfRes.Value.Count > 0)
            {
                //Create table
                foreach (var curPortfolio in portfRes.Value)
                {
                    var cells = new List<HTMLTable.Cell>();
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"/Portfolio/Remove/{0}\">remove</a>&nbsp;<a href=\"/Portfolio/Edit/{0}\">edit</a>&nbsp;<a href=\"/Portfolio/Dashboard/{0}\">{1}</a>", curPortfolio.Id, curPortfolio.Description) });
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
            var countriesRes = service.GetCountries();
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
                var res = service.AddPortfolio(model.Description, model.TimeZone);
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
                var countriesRes = service.GetCountries();
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
            var portfolioRes = service.Get(id);
            if (portfolioRes.HasError)
            {
                return View("Error");
            }
            else
            {
                var model = new PortfolioModel { 
                    Id = portfolioRes.Value.Id,
                    Description = portfolioRes.Value.Description,
                    TimeZone = portfolioRes.Value.TimeZone
                };
                ViewBag.Title = "Edit";
                var countriesRes = service.GetCountries();
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
                var res = service.Update(model.Id, model.Description, model.TimeZone);
                return RedirectToAction("");
            }
            else
            {
                return View("NewEdit");
            }
        }

        public ActionResult Remove(int id)
        {
            var res = service.Remove(id);
            if (res.HasError)
            {
                return View("Error");
            }
            else
            {
                return RedirectToAction("");
            }
        }

        public ActionResult Dashboard(int Id)
        {
            DateTime fromDate = DateTime.UtcNow.AddDays(-30);
            DateTime toDate = DateTime.UtcNow;
            var data = service.GetDashboardData(Id, fromDate, toDate);
            //Fill chart data
            var usageInitData = new List<object>();
            usageInitData.Add(new
            {
                data = data.Value.ViewsData.OrderBy(curItem => curItem.Key).Select(curItem => new object[] { curItem.Key.MilliTimeStamp(), curItem.Value }),
                color = "#461D7C"
            });
            ViewBag.UsageInitData = new JavaScriptSerializer().Serialize(usageInitData);
            ViewBag.PortfolioId = Id;
            ViewBag.PortfolioName = data.Value.PortfolioDescription;
            ViewBag.Applications = data.Value.Applications;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            return View();
        }

        public ActionResult Usage(int Id)
        {
            var reportRes = reportService.GetVisitsData(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow, Id, null, DataGrouping.Day);
            if (reportRes.HasError)
            {
                return View("Error");
            }
            //Fill chart data
            var chartInitData = new List<object>();
            chartInitData.Add(new
            {
                data = reportRes.Value.OrderBy(curItem => curItem.Key).Select(curItem => new object[] { curItem.Key.MilliTimeStamp(), curItem.Value }),
                color = "#461D7C"
            });
            ViewBag.ChartInitData = new JavaScriptSerializer().Serialize(chartInitData);
            ViewBag.PortfolioId = Id;
            return View();
        }

        public OperationResult<ReadOnlyCollection<TimeZoneInfo>> GetTimeZones()
        {
            return new OperationResult<ReadOnlyCollection<TimeZoneInfo>>(TimeZoneInfo.GetSystemTimeZones());
        }
    }
}

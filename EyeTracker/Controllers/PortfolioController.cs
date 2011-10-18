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

namespace EyeTracker.Controllers
{
    public class PortfolioController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IPortfolioService service;

        public PortfolioController()
            : this(new PortfolioService())
        {
        }

        public PortfolioController(IPortfolioService service)
        {
            this.service = service;
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
                    cells.Add(new HTMLTable.Cell() { Value = curPortfolio.Description });
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"\\Application\\{0}\" >{1}</a>", curPortfolio.Id, curPortfolio.Applications.Count) });
                    cells.Add(new HTMLTable.Cell() { Value = "0.00%" });
                }
            }
            else
            {
                data.Add(new List<HTMLTable.Cell>() { new HTMLTable.Cell() { ColSpan = 8, StyleClass = "no-data", Value = "No Transactions" } });
            }

            ViewData["caption"] = new HTMLTable.Cell() { Value = "Accounts" };
            ViewData["columnHeaders"] = columnHeaders;
            ViewData["data"] = data;
            return View();
        }

        public ActionResult New()
        {
            ViewBag.Title = "New";
            return View("NewEdit", new PortfolioModel());
        }

        [HttpPost]
        public ActionResult New(PortfolioModel model)
        {
            ViewBag.Title = "New";
            if (ModelState.IsValid)
            {
                return RedirectToAction("");
            }
            else
            {
                return View("NewEdit");
            }
        }

        public ActionResult Edit()
        {
            ViewBag.Title = "Edit";
            var countriesRes = service.GetCountries();
            if (!countriesRes.HasError)
            {
                ViewData["CountriesList"] = countriesRes.Value.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
                return View("NewEdit", new PortfolioModel());
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(PortfolioModel model)
        {
            ViewBag.Title = "Edit";
            if (ModelState.IsValid)
            {
                return RedirectToAction("");
            }
            else
            {
                return View("NewEdit");
            }
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            return View();
        }
    }
}

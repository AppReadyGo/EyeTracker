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
            var columnHeaders = new List<HTMLTable.Cell>() {
                    new HTMLTable.Cell() { Value = "Description" }, 
                    new HTMLTable.Cell() { Value = "" }, 
                    new HTMLTable.Cell() { Value = "" } 
                };
            var data = new List<List<HTMLTable.Cell>>();

            var portfRes = service.GetAll();
            if (0 != 0)
            {
                //decimal? balance = transList[0].Balance;
                //if (balance.HasValue)
                //{
                //    columnHeaders.Add(new HTMLTable.Cell() { Value = "Balance" });
                //}
                //columnHeaders.Add(new HTMLTable.Cell() { Value = "Actions" });
                //var curDate = DateTime.MaxValue;
                //HTMLTable.Cell curMonthCell = null;
                //HTMLTable.Cell curDayCell = null;
                //short monthRows = 0;
                //short dayRows = 0;
                ////Create table
                //foreach (var curTrans in transList)
                //{
                //    var cells = new List<HTMLTable.Cell>();
                //    if (curDate.Month != curTrans.Date.Month || curDate.Year != curTrans.Date.Year)
                //    {
                //        if (curMonthCell != null)
                //        {
                //            curMonthCell.RowSpan = monthRows;
                //        }
                //        monthRows = 0;
                //        curMonthCell = new HTMLTable.Cell() { Value = curTrans.Date.ToString("MMM yyyy"), StyleClass = "month-cell" };
                //        cells.Add(curMonthCell);
                //    }
                //    if (curDate.Day != curTrans.Date.Day || curDate.Month != curTrans.Date.Month || curDate.Year != curTrans.Date.Year)
                //    {
                //        if (curDayCell != null)
                //        {
                //            curDayCell.RowSpan = dayRows;
                //        }
                //        dayRows = 0;
                //        curDayCell = new HTMLTable.Cell() { Value = curTrans.Date.ToString("dd"), StyleClass = "day-cell" };
                //        cells.Add(curDayCell);
                //    }
                //    string id = curTrans.TypeId == 0 ? "" : curTrans.Id.ToString();
                //    cells.Add(new HTMLTable.Cell() { Value = id });
                //    cells.Add(new HTMLTable.Cell() { Value = GetPopupHtml(curTrans.Id, curTrans.Attachments, curTrans.Tags, curTrans.Notes) + curTrans.Description });
                //    string type = curTrans.TypeId == 0 ? "Analyzed" : curTrans.Type.ToString();
                //    cells.Add(new HTMLTable.Cell() { Value = type });
                //    string status = curTrans.TypeId == 0 ? "" : curTrans.Status.ToString();
                //    cells.Add(new HTMLTable.Cell() { Value = status });
                //    cells.Add(new HTMLTable.Cell() { Value = curTrans.Amount.ToString("£ 0.00"), StyleClass = curTrans.Amount > 0 ? "positive-amount" : "" });
                //    if (balance.HasValue)
                //    {
                //        balance += curTrans.Amount;
                //        cells.Add(new HTMLTable.Cell() { Value = balance.Value.ToString("£ 0.00"), StyleClass = Utilites.GetAmountClass(balance.Value) });
                //    }
                //    string actions = curTrans.TypeId == 0 ? string.Format("<a href=\"/Analysis/EditIntelTransaction/{0}\">Edit</a><input type=\"checkbox\" disabled=\"disabled\"/>", curTrans.Id) : string.Format("<a href=\"javascript:editTransaction({0});\" title=\"{2}\">Edit</a><input type=\"checkbox\" value=\"{0}\" sequence=\"{1}\"/>", curTrans.Id, curTrans.ScheduleId.HasValue ? "true" : "false", curTrans.ImportNote);
                //    cells.Add(new HTMLTable.Cell() { Value = actions });
                //    data.Add(cells);
                //    monthRows++;
                //    dayRows++;
                //    curDate = curTrans.Date;
                //}
                //curMonthCell.RowSpan = monthRows;
                //curDayCell.RowSpan = dayRows;

                //int pagesCount = (int)(transListRes.RowsCount / rowsOnPage);
                //if ((pagesCount * rowsOnPage) < transListRes.RowsCount) pagesCount++;
                //curPage = transListRes.CurPage;
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
            return View(new PortfolioModel());
        }

        [HttpPost]
        public ActionResult New(PortfolioModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("");
            }
            else
            {
                return View();
            }
        }


    }
}

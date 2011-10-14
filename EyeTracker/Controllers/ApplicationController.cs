using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using EyeTracker.Models;
using System.Drawing.Imaging;
using System.Runtime.Serialization.Json;
using EyeTracker.Common;
using EyeTracker.Model;
using EyeTracker.Core;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Core.Services;
using EyeTracker.DAL.Domain;
using EyeTracker.Helpers;
using EyeTracker.Domain.Model;

namespace EyeTracker.Controllers
{
    public class ApplicationController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IApplicationService service;
        private IPortfolioService portfolioService;
        private IAnalyticsService analyticsService;

        public ApplicationController()
            : this(new ApplicationService(), new PortfolioService(), new AnalyticsService())
        {
        }

        public ApplicationController(IApplicationService service, IPortfolioService portfolioService, IAnalyticsService analyticsService)
        {
            this.service = service;
            this.portfolioService = portfolioService;
            this.analyticsService = analyticsService;
        }

        public ActionResult Index()
        {
            var columnHeaders = new List<HTMLTable.Cell>() {
                    new HTMLTable.Cell() { Value = "Description" }, 
                    new HTMLTable.Cell() { Value = "" }, 
                    new HTMLTable.Cell() { Value = "" } 
                };
            var data = new List<List<HTMLTable.Cell>>();


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

        public ActionResult New(int portfolioId)
        {
            ViewBag.PortfolioId = portfolioId;
            ViewData["TypesList"] = Enum.GetValues(typeof(ApplicationType)).Cast<ApplicationType>().Select(i => new SelectListItem() { Text = i.ToString(), Value = ((int)i).ToString() });
            ViewBag.PackageLink = "http://mobillify.com";
            ViewBag.PropertyId = "MA-******-***";
            ViewBag.CodeSample = "<script type=\"text/javascript\">\nvar _gaq = _gaq || [];_\ngaq.push(['_setAccount', 'UA-1970564-12']);";
            return View(new ApplicationModel());
        }

        [HttpPost]
        public JsonResult New(ApplicationModel model)
        {
            OperationResult<string> res = null;
            if (ModelState.IsValid)
            {
                var portfolioRes = portfolioService.Get(model.PortfolioId);
                if (portfolioRes.HasError)
                {
                    res = new OperationResult<string>(portfolioRes);
                }
                else
                {
                    var app = new Application(portfolioRes.Value, model.Description, model.Type);
                    var appRes = service.Add(app);
                    if (appRes.HasError)
                    {
                    }
                    else
                    {
                        string key = "";
                        switch (app.Type)
                        {
                            case ApplicationType.Android:
                                key = "MA";
                                break;
                            case ApplicationType.Web:
                                key = "WP";
                                break;
                            case ApplicationType.iPhone:
                                key = "MI";
                                break;
                            case ApplicationType.WebMobile:
                                key = "WM";
                                break;
                            case ApplicationType.WindowsMobile:
                                key = "MW";
                                break;
                        }
                        res = new OperationResult<string>(string.Format("{0}-{1:000000}-{2:0000}", key, model.PortfolioId, appRes.Value));
                    }
                }
            }
            else
            {
                res = new OperationResult<string>();
            }
            return Json(res);
        }

        public FileResult JavaScriptFile(string filename)
        {
            log.WriteInformation("-->JavaScriptFile(filename:{0})", filename);

            //TODO: check client id
            var dir = Server.MapPath("/Scripts");
            var path = Path.Combine(dir, "AnalyticsTemplate.js");
            var file = new FileInfo(path);
            string content = string.Empty;
            if (file.Exists)
            {
                using (var stream = file.OpenText())
                {
                    content = stream.ReadToEnd();
                }
                string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Request.ApplicationPath == "/" ? "" : Request.ApplicationPath);
                content = content.Replace("{VISIT_HANDLER_URL}", url + Url.Action("Visit"));
                content = content.Replace("{PACKAGE_HANDLER_URL}", url +  Url.Action("Package"));
#if JSUNITTEST
                content = content.Replace("_mfyaq.init();", "");
#endif
            }
            log.WriteInformation("JavaScriptFile-->");
            return base.File(System.Text.Encoding.UTF8.GetBytes(content), "text/javascript");
        }

        public FileResult ClickHeatMapImage(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            byte[] imageData = null;
            log.WriteInformation("ClickHeatMapImage: appId:{0}, pageUri:{1}, clientWidth:{2}, clientHeight:{3}, fromDate:{4}, toDate:{5}", appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
            var opResult = analyticsService.GetClickHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
            if (!opResult.HasError)
            {
                Image bgImg = GetBackgroundImage(appId, clientWidth, clientHeight);
                Image image = HeatMapImage_.CreateClickHeatMap(opResult.Value, clientWidth, clientHeight, bgImg);
                using (MemoryStream mStream = new MemoryStream())
                {
                    image.Save(mStream, ImageFormat.Png);
                    imageData = mStream.ToArray();
                }
                image.Dispose();

            }
            return base.File(imageData, "Image/png");
        }

        public FileResult ViewHeatMapImage(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            byte[] imageData = null;
            log.WriteInformation("ViewHeatMapImage: appId:{0}, pageUri:{1}, clientWidth:{2}, clientHeight:{3}, fromDate:{4}, toDate:{5}",appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
            var opResult = analyticsService.GetViewHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
            if (!opResult.HasError)
            {
                Image bgImg = GetBackgroundImage(appId, clientWidth, clientHeight);
                Image image = HeatMapImage_.CreateViewHeatMap(opResult.Value, clientWidth, clientHeight, bgImg);
                using (MemoryStream mStream = new MemoryStream())
                {
                    image.Save(mStream, ImageFormat.Png);
                    imageData = mStream.ToArray();
                }
            }
            return base.File(imageData, "Image/png");
        }

        [HttpPost]
        public JsonResult Visit(VisitInfo visitInfo)
        {
            log.WriteInformation("-->Visit()");
            OperationResult<long> res = null;
            if (ModelState.IsValid)
            {
                visitInfo.Ip = Request.UserHostAddress;
#if JSUNITTEST
                res = new OperationResult<long>(1);
#else
                res = service.AddVisitInfo(visitInfo);
#endif
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var key in ModelState.Keys)
                {
                    var error = ModelState[key].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        sb.AppendFormat("{0} ", error.ErrorMessage);
                    }
                }
                res = new OperationResult<long>(ErrorNumber.WrongParameter, sb.ToString());
            }
            Response.AddHeader("Access-Control-Allow-Origin", "*");
            log.WriteInformation("Visit:{0}-->", res);
            return base.Json(res);
        }

    
        public JsonResult Debug(string json)
        {
            OperationResult<long> res = null;
            try
            {
                Messenger.SendEmail(null, new List<string>() { "ypanshin@gmail.com" }, "Found Driver License Practical Test Appointment", "On dates:"+json);
                res = new OperationResult<long>(ErrorNumber.None);
            }
            catch (Exception exp)
            {
                res = new OperationResult<long>(exp, "Debug");
            }
            return base.Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Package(PackageInfo packageInfo)
        {
            log.WriteInformation("-->Package(clicks:{0}, parts:{1})", packageInfo.clicks.Count, packageInfo.parts.Count);
            OperationResult res = null;
            try
            {
                res = new OperationResult();
                foreach (var curClikInfo in packageInfo.clicks)
                {
#if JSUNITTEST
                    var curRes = new OperationResult();
#else
                    var curRes = service.AddClickInfo(curClikInfo);
#endif
                    if (curRes.HasError) res = curRes;
                }
                foreach (var curViewPartInfo in packageInfo.parts)
                {
#if JSUNITTEST
                    var curRes = new OperationResult();
#else
                    var curRes = service.AddViewPartInfo(curViewPartInfo);
#endif
                    if (curRes.HasError) res = curRes;
                }

                Response.AddHeader("Access-Control-Allow-Origin", "*");
            }
            catch (Exception exp)
            {
                res = new OperationResult<long>(exp, "Package");
            }
            log.WriteInformation("Package:{0}-->", res);
            return base.Json(res);
        }

        private Image GetBackgroundImage(long appId, int clientWidth, int clientHeight)
        {
            string bgPath = Path.Combine(Server.MapPath("/Users_Resources/Screens"), string.Format("{0}.{1}.{2}.png", appId, clientWidth, clientHeight));
            Image bgImg = null;
            if (System.IO.File.Exists(bgPath)) bgImg = Image.FromFile(bgPath);
            return bgImg;
        }
    }
}

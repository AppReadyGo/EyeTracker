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

        public ActionResult Index(int portfolioId)
        {
            var appRes = service.GetAll(portfolioId);
            if (appRes.HasError)
            {
                return View("Error");
            }

            ViewBag.PortfolioId = portfolioId;
            var columnHeaders = new List<HTMLTable.Cell>() {
                    new HTMLTable.Cell() { Value = "Description" }, 
                    new HTMLTable.Cell() { Value = "Type" }, 
                    new HTMLTable.Cell() { Value = "% Change" },
                    new HTMLTable.Cell() { Value = "" } 
                };
            var data = new List<List<HTMLTable.Cell>>();

            if (appRes.Value.Count > 0)
            {
                //Create table
                foreach (var curApp in appRes.Value)
                {
                    var cells = new List<HTMLTable.Cell>();
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"/Application/Analyticst/{0}/{1}\">{2}</a>", portfolioId, curApp.Id, curApp.Description) });
                    cells.Add(new HTMLTable.Cell() { Value = curApp.Type.ToString() });
                    cells.Add(new HTMLTable.Cell() { Value = "0.00%" });
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"/Application/Edit/{0}/{1}\">edit</a>&nbsp;<a href=\"/Application/Remove/{0}/{1}\">remove</a>", portfolioId, curApp.Id) });
                    data.Add(cells);
                }
            }
            else
            {
                data.Add(new List<HTMLTable.Cell>() { new HTMLTable.Cell() { ColSpan = 8, StyleClass = "no-data", Value = "No Applications" } });
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
            ViewBag.PropertyId = "**-******-***";
            ViewBag.CodeSample = "<script type=\"text/javascript\">\nvar _gaq = _gaq || [];_\ngaq.push(['_setAccount', '**-******-***']);";
            return View("NewEdit",new ApplicationModel());
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
                        res = new OperationResult<string>(appRes);
                    }
                    else
                    {
                        string key = GetAppKey(app.Type);
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

        private static string GetAppKey(ApplicationType type)
        {
            string key = "";
            switch (type)
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
            return key;
        }

        public ActionResult Edit(int portfolioId, int appId)
        {
            var appRes = service.Get(appId);
            if (appRes.HasError)
            {
                return View("Error");
            }
            else
            {
                var app = appRes.Value;
                var model = new ApplicationModel { 
                    Id = app.Id,
                    Description = app.Description,
                    PortfolioId = portfolioId,
                    Type = app.Type
                };
                ViewBag.PortfolioId = portfolioId;
                ViewData["TypesList"] = Enum.GetValues(typeof(ApplicationType)).Cast<ApplicationType>().Select(i => new SelectListItem() { Text = i.ToString(), Value = ((int)i).ToString() });
                ViewBag.PackageLink = "http://mobillify.com";
                ViewBag.PropertyId = string.Format("{0}-{1:000000}-{2:0000}",GetAppKey(app.Type), portfolioId, appId);
                ViewBag.CodeSample = "<script type=\"text/javascript\">\nvar _gaq = _gaq || [];_\ngaq.push(['_setAccount', '" + ViewBag.PropertyId + "']);";
                return View("NewEdit", model);
            }
        }

        [HttpPost]
        public ActionResult Edit(ApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                var portfolioRes = portfolioService.Get(model.PortfolioId);
                if (portfolioRes.HasError)
                {
                    return View("Error");
                }
                else
                {
                    var appRes = service.Update(model.Id, model.Description, model.Type);
                    if (appRes.HasError)
                    {
                        return View("Error");
                    }
                    else
                    {
                        return RedirectToRoute("ApplicationDef");
                    }
                }
            }
            else
            {
                return View("NewEdit", model);
            }
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

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

namespace EyeTracker.Controllers
{
    public class ApplicationController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IAnalyticsService service;

        public ApplicationController()
            :this(new AnalyticsService())
        {
        }

        public ApplicationController(IAnalyticsService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
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
            var opResult = service.GetClickHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
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
            var opResult = service.GetViewHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
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

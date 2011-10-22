using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Domain.Model.Events;
using EyeTracker.Common;
using EyeTracker.Common.Logger;
using System.Reflection;
using System.Text;

namespace EyeTracker.Controllers
{
    public class EventsController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        
        [HttpPost]
        public JsonResult Visit(VisitEvent visitEvent)
        {
            log.WriteInformation("-->Visit()");
            OperationResult<long> res = null;
            if (ModelState.IsValid)
            {
                visitEvent.Ip = Request.UserHostAddress;
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
                Messenger.SendEmail(null, new List<string>() { "ypanshin@gmail.com" }, "Found Driver License Practical Test Appointment", "On dates:" + json);
                res = new OperationResult<long>(ErrorNumber.None);
            }
            catch (Exception exp)
            {
                res = new OperationResult<long>(exp, "Debug");
            }
            return base.Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Package(PackageEvent packageEvent)
        {
            log.WriteInformation("-->Package(clicks:{0}, parts:{1})", packageEvent.clicks.Count, packageEvent.parts.Count);
            OperationResult res = null;
            try
            {
                res = new OperationResult();
                foreach (var curClikInfo in packageEvent.clicks)
                {
#if JSUNITTEST
                    var curRes = new OperationResult();
#else
                    var curRes = service.AddClickInfo(curClikInfo);
#endif
                    if (curRes.HasError) res = curRes;
                }
                foreach (var curViewPartInfo in packageEvent.parts)
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

    }
}

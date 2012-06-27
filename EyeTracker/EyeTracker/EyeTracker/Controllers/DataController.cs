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
using EyeTracker.Core.Services;
using System.IO;

namespace EyeTracker.Controllers
{
    public class DataController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        
        //private IEventsServices eventsServices = null;

        //public DataController()
        //    : this(new EventsServices())
        //{
        //}

        //public DataController(IEventsServices eventsServices)
        //{
        //    this.eventsServices = eventsServices;
        //}

        #region Old code 
//        [HttpPost]
//        public JsonResult Visit(VisitEvent visitEvent)
//        {
//            log.WriteInformation("-->Visit()");
//            OperationResult<long> res = null;
//            if (ModelState.IsValid)
//            {
//                visitEvent.Ip = Request.UserHostAddress;
//#if JSUNITTEST
//                res = new OperationResult<long>(1);
//#else
//                res = eventsServices.AddVisitEvent(visitEvent);
//#endif
//            }
//            else
//            {
//                var sb = new StringBuilder();
//                foreach (var key in ModelState.Keys)
//                {
//                    var error = ModelState[key].Errors.FirstOrDefault();
//                    if (error != null)
//                    {
//                        sb.AppendFormat("{0} ", error.ErrorMessage);
//                    }
//                }
//                res = new OperationResult<long>(ErrorNumber.WrongParameter, sb.ToString());
//            }
//            Response.AddHeader("Access-Control-Allow-Origin", "*");
//            log.WriteInformation("Visit:{0}-->", res);
//            return base.Json(res);
//        }

        [HttpPost]
        public JsonResult Package(PackageEvent packageEvent)
        {
            //log.WriteInformation("-->Package(clicks:{0}, parts:{1})", packageEvent.clicks.Count, packageEvent.parts.Count);
            OperationResult res = null;
            try
            {
                res = new OperationResult();
#if JSUNITTEST
                var curRes = new OperationResult();
#else
                //var curRes = eventsServices.AddClickEvents(packageEvent.clicks);
#endif
                //if (curRes.HasError) res = curRes;

#if JSUNITTEST
                curRes = new OperationResult();
#else
                //curRes = eventsServices.AddViewPartEvents(packageEvent.parts);
#endif
                //if (curRes.HasError) res = curRes;

                Response.AddHeader("Access-Control-Allow-Origin", "*");
            }
            catch (Exception exp)
            {
                res = new OperationResult<long>(exp, "Package");
            }
            log.WriteInformation("Package:{0}-->", res);
            return base.Json(res);
        }

        #endregion Old Code

        ///// <summary>
        ///// Mobile event handler 
        ///// responsible to hable PackageEvent that contains mobile client data
        ///// <logic>
        ///// 1. Handle event
        ///// 2. call event services 
        ///// 3. return response 
        ///// </logic>
        ///// </summary>
        ///// <param name="packageEvent"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult Mobile(PackageEvent packageEvent)
        //{
        //    OperationResult res = default(OperationResult);
        //    try
        //    {
        //        res = new OperationResult();

        //        eventsServices.HandlePackageEvent(packageEvent);

        //        Response.AddHeader("Access-Control-Allow-Origin", "*");
        //    }
        //    catch (Exception exp)
        //    {
        //        res = new OperationResult<long>(exp, "Package");
        //    }
        //    log.WriteInformation("Package:{0}-->", res);
        //    return base.Json(res);
        //}


        /// <summary>
        /// REMARK : removed from DataController
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public FileResult Analytics(string filename)
        {
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
                content = content.Replace("{VISIT_HANDLER_URL}", url + "/Data/Visit/");
                content = content.Replace("{PACKAGE_HANDLER_URL}", url + "/Data/Package/");
#if JSUNITTEST
                content = content.Replace("_mfyaq.init();", "");
#endif
            }
            return base.File(System.Text.Encoding.UTF8.GetBytes(content), "text/javascript");
        }

    }

    #region OldCode
    /*
        public class DataController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        
        private IEventsServices eventsServices = null;

        public DataController()
            : this(new EventsServices())
        {
        }

        public DataController(IEventsServices eventsServices)
        {
            this.eventsServices = eventsServices;
        }

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
                res = eventsServices.AddVisitEvent(visitEvent);
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

        [HttpPost]
        public JsonResult Package(PackageEvent packageEvent)
        {
            log.WriteInformation("-->Package(clicks:{0}, parts:{1})", packageEvent.clicks.Count, packageEvent.parts.Count);
            OperationResult res = null;
            try
            {
                res = new OperationResult();
#if JSUNITTEST
                var curRes = new OperationResult();
#else
                var curRes = eventsServices.AddClickEvents(packageEvent.clicks);
#endif
                if (curRes.HasError) res = curRes;

#if JSUNITTEST
                curRes = new OperationResult();
#else
                curRes = eventsServices.AddViewPartEvents(packageEvent.parts);
#endif
                if (curRes.HasError) res = curRes;

                Response.AddHeader("Access-Control-Allow-Origin", "*");
            }
            catch (Exception exp)
            {
                res = new OperationResult<long>(exp, "Package");
            }
            log.WriteInformation("Package:{0}-->", res);
            return base.Json(res);
        }

        /// <summary>
        /// Mobile event handler 
        /// responsible to hable PackageEvent that contains mobile client data
        /// <logic>
        /// 1. Handle event
        /// 2. call event services 
        /// </logic>
        /// </summary>
        /// <param name="packageEvent"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Mobile(PackageEvent packageEvent)
        {
            OperationResult res = default(OperationResult);
            try
            {
                res = new OperationResult();
#if JSUNITTEST
                var curRes = new OperationResult();
#else
                var curRes = eventsServices.AddClickEvents(packageEvent.clicks);
#endif
                if (curRes.HasError) res = curRes;

#if JSUNITTEST
                curRes = new OperationResult();
#else
                curRes = eventsServices.AddViewPartEvents(packageEvent.parts);
#endif
                if (curRes.HasError) res = curRes;

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
     */

    #endregion
}

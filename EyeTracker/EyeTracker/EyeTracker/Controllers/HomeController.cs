using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EyeTracker.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "";//long appId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate
            ViewData["viewHeatMapImgUrl"] = Url.Action("ViewHeatMapImage", "Analytics", new RouteValueDictionary(new { appId = 3, pageUri = "http://mobillify.com/"/*http://localhost:1058/Test.htm"*/, clientWidth = 980, clientHeight = 1249, fromDate = DateTime.Now.AddYears(-1), toDate = DateTime.Now.AddYears(1) }));
            ViewData["clickHeatMapImgUrl"] = Url.Action("ClickHeatMapImage", "Analytics", new RouteValueDictionary(new { appId = 3, pageUri = "http://mobillify.com/"/*http://localhost:1058/Test.htm"*/, clientWidth = 980, clientHeight = 1249, fromDate = DateTime.Now.AddYears(-1), toDate = DateTime.Now.AddYears(1) }));

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

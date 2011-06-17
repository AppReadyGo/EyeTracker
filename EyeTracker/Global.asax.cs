using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EyeTracker
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "JavaScript", // Route name
                "Analytics/JavaScript/{clientId}.js", // URL with parameters
                new { controller = "Analytics", action = "JavaScriptFile" } // Parameter defaults
            );
            routes.MapRoute(
                "AjaxVisit", // Route name
                "Analytics/{action}/{json}", // URL with parameters
                new { controller = "Analytics" } // Parameter defaults
            );/*
            routes.MapRoute(
                "AjaxPackage", // Route name
                "Analytics/Package/{json}", // URL with parameters
                new { controller = "Analytics", action = "Package" } // Parameter defaults
            );*/
            routes.MapRoute(
                "ClickHeatMapImage", // Route name
                "Analytics/{action}/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", // URL with parameters
                new { controller = "Analytics" } // Parameter defaults
            );/*
            routes.MapRoute(
                "ViewHeatMapImage", // Route name
                "Analytics/ViewHeatMapImage/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", // URL with parameters
                new { controller = "Analytics", action = "ViewHeatMapImage" } // Parameter defaults
            );*/

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}
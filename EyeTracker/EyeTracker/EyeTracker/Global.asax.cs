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
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "JavaScript", // Route name
                "{controller}/{action}/{clientId}", // URL with parameters
                new { controller = "Analytics", action = "JavaScript" } // Parameter defaults
            );
            routes.MapRoute(
                "AjaxVisit", // Route name
                "{controller}/{action}/{json}", // URL with parameters
                new { controller = "Analytics", action = "Visit" } // Parameter defaults
            );
            routes.MapRoute(
                "AjaxPackage", // Route name
                "{controller}/{action}/{json}", // URL with parameters
                new { controller = "Analytics", action = "Package" } // Parameter defaults
            );
            routes.MapRoute(
                "ClickHeatMapImage", // Route name
                "{controller}/{action}/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", // URL with parameters
                new { controller = "Analytics", action = "ClickHeatMapImage" } // Parameter defaults
            );
            routes.MapRoute(
                "ViewHeatMapImage", // Route name
                "{controller}/{action}/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", // URL with parameters
                new { controller = "Analytics", action = "ViewHeatMapImage" } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}
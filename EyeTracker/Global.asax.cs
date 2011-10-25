using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EyeTracker.CustomModelBinders;
using Castle.Windsor;
using EyeTracker.Windsor;
using EyeTracker.Model;
using EyeTracker.DAL.Domain;
using EyeTracker.Domain;
using EyeTracker.Domain.Model.Events;

namespace EyeTracker
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        WindsorContainer applicationWideWindsorContainer = new WindsorContainer();
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "JavaScript",
                "Data/Analytics/{key}.js",
                new { controller = "Data", action = "Analytics" }
            );
            routes.MapRoute(
                "AjaxData", // Route name
                "Data/{action}/", // URL with parameters
                new { controller = "Data" } // Parameter defaults
            );

            routes.MapRoute(
                "Home", // Route name
                "", // URL with parameters
                new { controller = "Home", action = "Index" } // Parameter defaults
            );
            routes.MapRoute(
                "Admin_elmah",
                "Admin/Elmah/{query}",
                new { action = "Elmah", controller = "Admin", query = UrlParameter.Optional }
            );
            routes.MapRoute(
               "Admin_edit",
               "Admin/{action}/{id}",
               new { action = "UserEdit", controller = "Admin" }
            );
            routes.MapRoute(
                "Account", // Route name
                "Account/{action}", // URL with parameters
                new { controller = "Account", action = "LogOn" } 
            );
            routes.MapRoute(
                "Application", // Route name
                "Application/{action}/{portfolioId}/{appId}", // URL with parameters
                new { controller = "Application", action = "Index", appId = UrlParameter.Optional }
            );
            routes.MapRoute(
                "ApplicationDef", // Route name
                "Application/{portfolioId}", // URL with parameters
                new { controller = "Application", action = "Index" }
            );
            routes.MapRoute(
                "Portfolio", // Route name
                "Portfolio/{action}/{portfolioId}", // URL with parameters
                new { controller = "Portfolio", action = "Index", portfolioId = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Analytics", // Route name
                "Analytics/{action}", // URL with parameters
                new { controller = "Analytics", action = "EyeTracker" }
            );
            routes.MapRoute(
                "Content", // Route name
                "{urlPart1}/{urlPart2}/{urlPart3}", // URL with parameters
                new { controller = "Home", action = "PageContent", urlPart2 = UrlParameter.Optional, urlPart3 = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    "JavaScript", 
            //    "Analytics/JavaScript/{filename}.js",
            //    new { controller = "Analytics", action = "JavaScriptFile" } 
            //);
            //routes.MapRoute(
            //    "AjaxVisit", // Route name
            //    "Analytics/{action}/{json}", // URL with parameters
            //    new { controller = "Analytics" } // Parameter defaults
            //);/*
            //routes.MapRoute(
            //    "AjaxPackage", // Route name
            //    "Analytics/Package/{json}", // URL with parameters
            //    new { controller = "Analytics", action = "Package" } // Parameter defaults
            //);*/
            //routes.MapRoute(
            //    "ClickHeatMapImage", // Route name
            //    "Analytics/{action}/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", // URL with parameters
            //    new { controller = "Analytics" } // Parameter defaults
            //);/*
            //routes.MapRoute(
            //    "ViewHeatMapImage", // Route name
            //    "Analytics/ViewHeatMapImage/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", // URL with parameters
            //    new { controller = "Analytics", action = "ViewHeatMapImage" } // Parameter defaults
            //);*/

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders[typeof(VisitEvent)] = new JsonVisitInfoModelBinder();
            ModelBinders.Binders[typeof(PackageEvent)] = new JsonPackageModelBinder();

            using (var session = NHibernateHelper.OpenSession())
            {
            }
            //ControllerBuilder.Current.SetControllerFactory(new WindsorFactory(applicationWideWindsorContainer));
            //// Initialize / install components in container
            //applicationWideWindsorContainer.Install(new WindsorInstaller());
        }
    }
}
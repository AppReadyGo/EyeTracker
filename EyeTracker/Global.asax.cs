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
using EyeTracker.Common;
using EyeTracker.Model.Pages.Analytics;
using EyeTracker.Core;

namespace EyeTracker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        WindsorContainer applicationWideWindsorContainer = new WindsorContainer();
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "JavaScript",
                "Analytics/{filename}.js",
                new { controller = "Data", action = "Analytics" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "AjaxData", 
                "Data/{action}/",
                new { controller = "Data" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Home", 
                "",
                new { controller = "Home", action = "Index" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Admin",
                "Admin/{action}",
                new { action = "Index", controller = "Admin" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Admin_elmah",
                "Admin/Elmah/{query}",
                new { action = "Elmah", controller = "Admin", query = UrlParameter.Optional },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
               "Admin_edit",
               "Admin/{action}/{id}",
               new { action = "UserEdit", controller = "Admin" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "AccountChangePassword",
                "Account/ChangePassword",
                new { controller = "Secure", action = "ChangePassword" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Account",
                "Account/{action}",
                new { controller = "Account", action = "LogOn" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Activation",
                "Account/{action}/{key}",
                new { controller = "Account", action = "Activate" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "ApplicationScreen", 
                "Application/Screen/{appId}/{width}/{height}/{file}",
                new { controller = "Application", action = "Screen", appId = UrlParameter.Optional },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "ApplicationClickHeatMapImage",
                "Application/ClickHeatMapImage/{appId}/{pageUri}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}/{preview}",
                new { controller = "Application", action = "ClickHeatMapImage" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "ApplicationViewHeatMapImage",
                "Application/ViewHeatMapImage/{appId}/{pageUri}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}/{preview}",
                new { controller = "Application", action = "ViewHeatMapImage" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Application", 
                "Application/{action}/{id}",
                new { controller = "Application", action = "New" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "ApplicationDef", 
                "Application/{portfolioId}",
                new { controller = "Application", action = "Index" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Portfolio",
                "Portfolio/{action}/{id}",
                new { controller = "Portfolio", action = "Index", id = UrlParameter.Optional },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "AnalyticsData",
                "Analytics/{action}/{portfolioId}/{applicationId}/{fromDate}/{toDate}/{screenSize}/{path}/{language}/{os}/{location}",
                new { controller = "Analytics", action = "Dashboard", applicationId = UrlParameter.Optional, fromDate = UrlParameter.Optional, toDate = UrlParameter.Optional, screenSize = UrlParameter.Optional, path = UrlParameter.Optional, language = UrlParameter.Optional, os = UrlParameter.Optional, location = UrlParameter.Optional },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Analytics",
                "Analytics/{action}/{id}",
                new { controller = "Analytics", action = "Index", id = UrlParameter.Optional },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Secure",
                "Secure/{action}",
                new { controller = "Secure", action = "ChangePassword" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "HomeRoute",
                "Home/{action}",
                new { controller = "Home", action = "Index" },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            //-----  Content routes
            routes.MapRoute(
               "404",
               "404",
               new { controller = "Content", action = "ErrorPage404" },
               new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
               "Error",
               "Error",
               new { controller = "Content", action = "ErrorPage" },
               new[] { "EyeTracker.Controllers" } // Namespaces
            );
            
            routes.MapRoute(
                "CSS", // Route name
                "content/css/{path1}/{path2}/{path3}/{path4}/{path5}/{path6}/{path7}", // URL with parameters
                new { controller = "Content", action = "css", path2 = UrlParameter.Optional, path3 = UrlParameter.Optional, path4 = UrlParameter.Optional, path5 = UrlParameter.Optional, path6 = UrlParameter.Optional, path7 = UrlParameter.Optional }, // Parameter defaults
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Mails",
                "m/{urlPart1}/{urlPart2}/{urlPart3}/{isMail}",
                new { controller = "Content", action = "MailContent", urlPart2 = UrlParameter.Optional, urlPart3 = UrlParameter.Optional, isMail = UrlParameter.Optional },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Pages",
                "p/{urlPart1}/{urlPart2}/{urlPart3}/",
                new { controller = "Content", action = "PageContent", urlPart2 = UrlParameter.Optional, urlPart3 = UrlParameter.Optional },
                new[] { "EyeTracker.Controllers" } // Namespaces
            );


            //routes.MapRoute(
            //    "JavaScript", 
            //    "Analytics/JavaScript/{filename}.js",
            //    new { controller = "Analytics", action = "JavaScriptFile" } 
            //);
            //routes.MapRoute(
            //    "AjaxVisit", 
            //    "Analytics/{action}/{json}", 
            //    new { controller = "Analytics" } 
            //);/*
            //routes.MapRoute(
            //    "AjaxPackage", 
            //    "Analytics/Package/{json}", 
            //    new { controller = "Analytics", action = "Package" } 
            //);*/
            //routes.MapRoute(
            //    "ClickHeatMapImage", 
            //    "Analytics/{action}/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", 
            //    new { controller = "Analytics" } 
            //);/*
            //routes.MapRoute(
            //    "ViewHeatMapImage", 
            //    "Analytics/ViewHeatMapImage/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", 
            //    new { controller = "Analytics", action = "ViewHeatMapImage" } 
            //);*/

            //routes.MapRoute(
            //    "Default", 
            //    "{controller}/{action}/{id}", 
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } 
            //);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            // GlobalFilters.Filters.Add(new CSSAttribute());
            GlobalFilters.Filters.Add(new RedirectToMobileAttribute(), 1);


            ModelBinders.Binders[typeof(FilterParametersModel)] = new FilterParametersModelBinder();

            ObjectContainer.Instance.GetType();
            //ControllerBuilder.Current.SetControllerFactory(new WindsorFactory(applicationWideWindsorContainer));
            //// Initialize / install components in container
            //applicationWideWindsorContainer.Install(new WindsorInstaller());
        }
    }
}
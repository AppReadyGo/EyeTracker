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
                "Analytics/{filename}.js",
                new { controller = "Data", action = "Analytics" }
            );

            routes.MapRoute(
                "AjaxData", 
                "Data/{action}/", 
                new { controller = "Data" } 
            );

            routes.MapRoute(
                "Home", 
                "", 
                new { controller = "Home", action = "Index" } 
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
                "Account", 
                "Account/{action}", 
                new { controller = "Account", action = "LogOn" } 
            );

            routes.MapRoute(
                "ApplicationScreen", 
                "Application/Screen/{appId}/{width}/{height}/{file}", 
                new { controller = "Application", action = "Screen", appId = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ApplicationClickHeatMapImage",
                "Application/ClickHeatMapImage/{appId}/{pageUri}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}/{preview}",
                new { controller = "Application", action = "ClickHeatMapImage" }
            );

            routes.MapRoute(
                "ApplicationViewHeatMapImage",
                "Application/ViewHeatMapImage/{appId}/{pageUri}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}/{preview}",
                new { controller = "Application", action = "ViewHeatMapImage" }
            );

            routes.MapRoute(
                "Application", 
                "Application/{action}/{portfolioId}/{appId}", 
                new { controller = "Application", action = "Index", appId = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ApplicationDef", 
                "Application/{portfolioId}", 
                new { controller = "Application", action = "Index" }
            );

            routes.MapRoute(
                "Portfolio", 
                "Portfolio/{action}/{id}",
                new { controller = "Portfolio", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Analytics", 
                "Analytics/{action}", 
                new { controller = "Analytics", action = "EyeTracker" }
            );

            routes.MapRoute(
                "Content", 
                "{urlPart1}/{urlPart2}/{urlPart3}/", 
                new { controller = "Home", action = "PageContent", urlPart2 = UrlParameter.Optional, urlPart3 = UrlParameter.Optional }
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

            //ModelBinders.Binders[typeof(VisitEvent)] = new JsonVisitInfoModelBinder();
            //ModelBinders.Binders[typeof(PackageEvent)] = new JsonPackageModelBinder();
            ModelBinders.Binders[typeof(PackageEvent)] = new JsonMobileDataModelBinder();

            using (var session = NHibernateHelper.OpenSession())
            {
            }
            //ControllerBuilder.Current.SetControllerFactory(new WindsorFactory(applicationWideWindsorContainer));
            //// Initialize / install components in container
            //applicationWideWindsorContainer.Install(new WindsorInstaller());
        }
    }
}
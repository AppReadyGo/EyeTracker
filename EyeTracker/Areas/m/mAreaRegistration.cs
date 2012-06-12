using System.Web.Mvc;

namespace EyeTracker.Areas.m
{
    public class mAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "m";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "m_default",
                "m",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "EyeTracker.Areas.m.Controllers" } // Namespaces            
            );

            context.MapRoute(
                "m_account",
                "m/account/{action}",
                new { controller = "Account", action = "Register" },
                new[] { "EyeTracker.Areas.m.Controllers" } // Namespaces            
            );

            context.MapRoute(
                "m_content",
                "m/{urlPart1}/{urlPart2}/{urlPart3}/",
                new { controller = "Home", action = "PageContent", urlPart2 = UrlParameter.Optional, urlPart3 = UrlParameter.Optional },
                new[] { "EyeTracker.Areas.m.Controllers" } // Namespaces
            );
        }
    }
}

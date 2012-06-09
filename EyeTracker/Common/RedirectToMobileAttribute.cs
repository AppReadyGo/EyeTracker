using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web.Routing;

namespace EyeTracker.Common
{
    public class RedirectToMobileAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            // Only redirect on the first request in a session
            if (!httpContext.Session.IsNewSession)
                return true;

            // Don't redirect non-mobile browsers
            if (!IsMobile(httpContext.Request))
                return true;

            // Don't redirect requests for the Mobile area
            if (Regex.IsMatch(httpContext.Request.Url.PathAndQuery, "/m($|/)"))
                return true;

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var redirectionRouteValues = GetRedirectionRouteValues(filterContext.RequestContext);
            filterContext.Result = new RedirectToRouteResult(redirectionRouteValues);
        }

        // Override this method if you want to customize the controller/action/parameters to which
        // mobile users would be redirected. This lets you redirect users to the mobile equivalent
        // of whatever resource they originally requested.
        protected virtual RouteValueDictionary GetRedirectionRouteValues(RequestContext requestContext)
        {
            return new RouteValueDictionary(new { area = "m", controller = "Home", action = "Index" });
        }

        private bool IsMobile(HttpRequestBase request)
        {
            bool isMobile = request.Browser.IsMobileDevice;
            string userAgent = request.UserAgent.ToLower();

            if (
                // Check for mobile devices
                isMobile
                || userAgent.Contains("iphone")
                || userAgent.Contains("ipod")
                || userAgent.Contains("blackberry")
                || userAgent.Contains("mobile")
                || userAgent.Contains("windows ce")
                || userAgent.Contains("opera mini")
                || userAgent.Contains("palm")
                    )
            {
                return true;
            }

            return false;
        }
    }
}
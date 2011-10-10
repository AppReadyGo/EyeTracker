using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmah;

namespace EyeTracker.Controllers
{
    public class ErrorController : Controller
    {
        [HttpPost]
        public void LogJavaScriptError(string message)
        {
            ErrorSignal.FromCurrentContext().Raise(new JavaScriptException(message));
        }

        [HttpPost]
        public void LogClientAPIError(string message)
        {
            ErrorSignal.FromCurrentContext().Raise(new ClientAPIException(message));
        }
    }

}

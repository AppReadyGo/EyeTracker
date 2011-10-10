using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Controllers
{
    public class JavaScriptException : Exception
    {
        public JavaScriptException(string message)
            : base(message)
        {
        }
    }
    public class ClientAPIException : Exception
    {
        public ClientAPIException(string message)
            : base(message)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Domain.Model.Events;
using EyeTracker.Common;
using EyeTracker.Common.Logger;
using System.Reflection;
using System.Text;
using EyeTracker.Core.Services;
using System.IO;

namespace EyeTracker.Controllers
{
    public class FilesController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
 
        public FileResult Analytics(string filename)
        {
            //TODO: check client id
            var dir = Server.MapPath("~/Scripts");
            var path = Path.Combine(dir, "AnalyticsTemplate.js");
            var file = new FileInfo(path);
            string content = string.Empty;
            if (file.Exists)
            {
                using (var stream = file.OpenText())
                {
                    content = stream.ReadToEnd();
                }
                string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Request.ApplicationPath == "/" ? "" : Request.ApplicationPath);
                content = content.Replace("{VISIT_HANDLER_URL}", url + "/Data/Visit/");
                content = content.Replace("{PACKAGE_HANDLER_URL}", url + "/Data/Package/");
#if JSUNITTEST
                content = content.Replace("_mfyaq.init();", "");
#endif
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
            return base.File(System.Text.Encoding.UTF8.GetBytes(content), "text/javascript");
        }

        [Authorize]
        public FileResult Packages(string filename)
        {
            string packagePath = Server.MapPath(string.Format("~/Restricted/Packages/{0}.jar", filename));
            var file = new FileInfo(packagePath);
            if (file.Exists)
            {
                using (var fs = file.OpenRead())
                {
                    return base.File(fs, "application/java-archive", filename + ".jar");
                }
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }
    }
}

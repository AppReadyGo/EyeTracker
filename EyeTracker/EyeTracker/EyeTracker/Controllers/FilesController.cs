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
using System.Configuration;
using EyeTracker.Common.Entities;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

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
            string packagePath = Server.MapPath(string.Format("~/Restricted/Packages/{0}", filename));

            if (System.IO.File.Exists(packagePath))
            {
                var contentType = Path.GetExtension(filename) == ".jar" ? "application/java-archive" : "application/octet-stream";
                return base.File(packagePath, contentType, filename);
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }

        [Authorize]
        public FileResult Screens(string filename)
        {
            string screenPath = Server.MapPath(string.Format("~/Restricted/Screens/{0}", filename));

            if (System.IO.File.Exists(screenPath))
            {
                var contentType = "image/" + (Path.GetExtension(filename) == ".png" ? "png" : "jpeg");
                return base.File(screenPath, contentType, filename);
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }

        [Authorize]
        public FileResult Properties(ApplicationType type, int pId, int appId, string filename)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("FingerPrint={0}", ConfigurationManager.AppSettings["FingerprintEnabled"]).AppendLine();
            sb.AppendFormat("AllowSend3G={0}", ConfigurationManager.AppSettings["AllowSend3G"]).AppendLine();
            sb.AppendFormat("ApplicationName={0}", ApplicationController.GetAppKey(type, pId, appId)).AppendLine();
            var cacheInDatabase = ConfigurationManager.AppSettings["CacheInDatabase"];
            if (!string.IsNullOrEmpty(cacheInDatabase))
            {
                sb.AppendFormat("CacheInDatabase={0}", cacheInDatabase).AppendLine();
            }
            var servermode = ConfigurationManager.AppSettings["ServerMode"];
            if (!string.IsNullOrEmpty(servermode))
            {
                sb.AppendFormat("ServerMode={0}", servermode).AppendLine();
            }
            return base.File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "application/octet-stream");
        }

        [Authorize]
        public FileResult Thumbnails(string filename)
        {
            byte[] imageData = null;
            var dir = Server.MapPath("~/Restricted/Screens");
            var path = Path.Combine(dir, filename);
            var file = new FileInfo(path);
            if (file.Exists)
            {
                var img = resizeImage(new Bitmap(path), 100);

                using (MemoryStream mStream = new MemoryStream())
                {
                    img.Save(mStream, ImageFormat.Png);
                    imageData = mStream.ToArray();
                }
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
            return base.File(imageData, "Image/png");
        }

        private static Image resizeImage(Image imgToResize, int longEdgeSize)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            int destWidth = longEdgeSize;
            int destHeight = longEdgeSize;

            if (sourceWidth < sourceHeight)
                destWidth = sourceWidth * destHeight / sourceHeight;
            else
                destHeight = sourceHeight * destWidth / sourceWidth;

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
    }
}
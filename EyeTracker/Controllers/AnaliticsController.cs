using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using EyeTracker.Models;
using System.Drawing.Imaging;
using System.Runtime.Serialization.Json;
using EyeTracker.Core.Models;
using EyeTracker.Common;
using EyeTracker.Model;

namespace EyeTracker.Controllers
{
    public class AnaliticsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public FileResult JavaScript()
        {
            var dir = Server.MapPath("/Scripts");
            var path = Path.Combine(dir, "AnaliticsTemplate.js");
            var file = new FileInfo(path);
            string content = string.Empty;
            if (file.Exists)
            {
                using (var stream = file.OpenText())
                {
                    content = stream.ReadToEnd();
                    content = content.Replace("{HANDLER_URL}", Url.Action("Data"));
                    content = content.Replace("{CLIENT_ID}", "");
                }
            }
            return base.File(System.Text.Encoding.UTF8.GetBytes(content), "text/javascript");
        }

        public FileResult ClickHeatMapImage()
        {
            Image image = HeatMapImage.CreateClickHeatMap(null, 0, 0, 0, 0, (Image)new Bitmap(0, 0));
            byte[] imageData = null;
            using (MemoryStream mStream = new MemoryStream())
            {
                image.Save(mStream, ImageFormat.Png);
                imageData = mStream.ToArray();
            }

            return base.File(imageData, "Image/png");
        }

        public FileResult ViewHeatMapImage()
        {
            Image image = HeatMapImage.CreateViewHeatMap(null, 0, 0, 0, 0, (Image)new Bitmap(0, 0));
            byte[] imageData = null;
            using (MemoryStream mStream = new MemoryStream())
            {
                image.Save(mStream, ImageFormat.Png);
                imageData = mStream.ToArray();
            }

            return base.File(imageData, "Image/png");
        }


        public JsonResult Visit()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(VisitInfoViewModel));

            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(Request["d"]));
            VisitInfoViewModel visitInfo = serializer.ReadObject(ms) as VisitInfoViewModel;
            if (string.IsNullOrEmpty(visitInfo.Uri))
            {
                visitInfo.Uri = Request.Url.ToString();
            }
            //TODO: Add the visit to db
            OperationResult<long> res = null;

            return base.Json(res.Value);
        }

        public JsonResult Package()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AnalyticsPackage));

            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(Request["d"]));
            AnalyticsPackage packageObject = serializer.ReadObject(ms) as AnalyticsPackage;
            //TODO: Add the package to db
            OperationResult res = null;

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EyeTracker.Core;
using AutoMapper;
using EyeTracker.DAL.Models;

namespace EyeTracker.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        IAnalyticsService service;
        public HomeController()
            : this( new AnalyticsService())
        {
        }

        public HomeController(IAnalyticsService service)
        {
            this.service = service;
        }

        public ActionResult Clear(long appId, string pageUri, string clientSize)
        {
            string[] split = clientSize.Split('|');
            int width = int.Parse(split[0]);
            int height = int.Parse(split[1]);
            var analyticsInfoRes = service.ClearAnalytics("CED35BCA-3CC4-425B-A042-6ABCC2C6F250", appId, pageUri, width, height);
            return RedirectToAction("Index");
        }

        public ActionResult Index(long? appId, string pageUri, string clientSize)
        {
            var analyticsInfoRes = service.GetAnalyticsInfo("CED35BCA-3CC4-425B-A042-6ABCC2C6F250", appId, pageUri);
            List<SelectListItem> applications = null;
            List<SelectListItem> uriList = null;
            List<SelectListItem> clientSizes = null;
            if (!analyticsInfoRes.WasError)
            {
                Mapper.CreateMap<KeyValuePair<long, string>, SelectListItem>()
                   .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Value))
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Key));
                applications = Mapper.Map<Dictionary<long, string>, List<SelectListItem>>(analyticsInfoRes.Value.Applications);
                Mapper.CreateMap<string, SelectListItem>()
                   .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src))
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src));
                uriList = Mapper.Map<List<string>, List<SelectListItem>>(analyticsInfoRes.Value.UriList);
                Mapper.CreateMap<AnalyticsInfo.Size, SelectListItem>()
                   .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Width + "X" + src.Height))
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Width + "|" + src.Height));
                clientSizes = Mapper.Map<List<AnalyticsInfo.Size>, List<SelectListItem>>(analyticsInfoRes.Value.ClientSizes);
            }
            else
            {
                ViewData["errorMessage"] = analyticsInfoRes.ErrorMessage;
            }
            string viewHeatMapImgUrl = string.Empty;
            string clickHeatMapImgUrl = string.Empty;
            if (!string.IsNullOrEmpty(clientSize))
            {
                string[] split = clientSize.Split('|');
                int width = int.Parse(split[0]);
                int height = int.Parse(split[1]);
                viewHeatMapImgUrl = Url.Action("ViewHeatMapImage", "Analytics", new RouteValueDictionary(new { appId = appId, pageUri = pageUri, clientWidth = width, clientHeight = height, fromDate = DateTime.Now.AddYears(-1), toDate = DateTime.Now.AddYears(1) }));
                clickHeatMapImgUrl = Url.Action("ClickHeatMapImage", "Analytics", new RouteValueDictionary(new { appId = appId, pageUri = pageUri, clientWidth = width, clientHeight = height, fromDate = DateTime.Now.AddYears(-1), toDate = DateTime.Now.AddYears(1) }));
                ViewData["width"] = width > 510 ? 510 : width;
                ViewData["height"] = height;
            }
            else
            {
                ViewData["width"] = 0;
                ViewData["height"] = 0;
            }
            ViewData["applications"] = applications ?? new List<SelectListItem>();
            ViewData["uriList"] = uriList ?? new List<SelectListItem>();
            ViewData["clientSizes"] = clientSizes ?? new List<SelectListItem>();

            ViewData["viewHeatMapImgUrl"] = viewHeatMapImgUrl;
            ViewData["clickHeatMapImgUrl"] = clickHeatMapImgUrl;

            ViewData["appId"] = appId;
            ViewData["pageUri"] = pageUri;
            ViewData["clientSize"] = clientSize;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

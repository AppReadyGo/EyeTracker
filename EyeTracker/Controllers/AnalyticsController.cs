using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EyeTracker.Common;
using EyeTracker.Common.Logger;
using EyeTracker.Common.Queries.Analytics;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Core;
using EyeTracker.Core.Services;
using EyeTracker.Model.Filter;
using EyeTracker.Model.Master;
using EyeTracker.Model.Pages.Analytics;
using EyeTracker.Model.Pages.Home;
using EyeTracker.Models;

namespace EyeTracker.Controllers
{
    [Authorize]
    public class AnalyticsController : FilterController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        
        private IAnalyticsService analyticsService;

        public AnalyticsController()
            : this(new AnalyticsService())
        {
        }

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            this.analyticsService = analyticsService;
        }

        public override AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.MenuItem.Analytics; }
        }

        public ActionResult Index()
        {
            var portfolios = ObjectContainer.Instance.RunQuery(new PortfoliosQuery());
            return View(new IndexViewModel(portfolios.PortfoliosData), AnalyticsMasterModel.MenuItem.Portfolios);
        }

        public ActionResult Dashboard(FilterParametersModel filter)
        {
            log.WriteInformation("Dashboard");

            filter.Validate();

            string[] splitedScreenSize = string.IsNullOrEmpty(filter.ss) ? null : filter.ss.Split(new char[] { 'X' });
            int? sw = splitedScreenSize == null ? null : (int?)int.Parse(splitedScreenSize[0]);
            int? sh = splitedScreenSize == null ? null : (int?)int.Parse(splitedScreenSize[1]);

            var dashboardViewData = ObjectContainer.Instance.RunQuery(
                new DashboardViewDataQuery(filter.fd.Value,
                                    filter.td.Value,
                                    filter.pid,
                                    filter.aid,
                                    sh,
                                    sw,
                                    filter.p,
                                    filter.l,
                                    filter.os,
                                    filter.c,
                                    filter.ct,
                                    DataGrouping.Day));

            //Grouping data by day. To show on graph all days from start till end.
            var visitsData = new List<object[]>();
            int diffDays = (filter.td.Value - filter.fd.Value).Days;
            for (int i = 0; i < diffDays; i++)
            {
                int count = 0;
                var curDate = filter.fd.Value.AddDays(i);
                if (dashboardViewData.Data.ContainsKey(curDate))
                {
                    count = dashboardViewData.Data[curDate];
                }
                visitsData.Add(new object[] { curDate.MilliTimeStamp(), count });
            }

            //Create chart data
            var usageInitData = new List<object>();
            usageInitData.Add(new
            {
                data = visitsData,
                color = "#461D7C"
            });

            var dashboardModel = new DashboardModel
            {
                UsageChartData = new JavaScriptSerializer().Serialize(usageInitData),
                ContentOverviewData = dashboardViewData.ContentOverview
            };

            return View(dashboardModel, AnalyticsMasterModel.MenuItem.Dashboard, dashboardViewData, filter);
        }

        public ActionResult Usage(FilterParametersModel filter)
        {
            filter.Validate();

            var query = new UsageViewDataQuery(
                filter.fd.Value,
                filter.td.Value,
                filter.pid,
                filter.aid,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                DataGrouping.Day);

            var usageViewData = ObjectContainer.Instance.RunQuery(query);

            //Grouping data by day. To show on graph all days from start till end.
            var data = new List<object[]>();
            int diffDays = (filter.td.Value - filter.fd.Value).Days;
            for (int i = 0; i < diffDays; i++)
            {
                int count = 0;
                var curDate = filter.fd.Value.AddDays(i);
                if (usageViewData.Data.ContainsKey(curDate))
                {
                    count = usageViewData.Data[curDate];
                }
                data.Add(new object[] { curDate.MilliTimeStamp(), count });
            }

            //Create chart data
            var usageInitData = new List<object>();
            usageInitData.Add(new
            {
                data = data,
                color = "#461D7C"
            });

            var model = new UsageModel { UsageChartData = new JavaScriptSerializer().Serialize(usageInitData) };

            return View(model, AnalyticsMasterModel.MenuItem.Usage, usageViewData, filter);
        }

        public ActionResult FingerPrint(FilterParametersModel filter)
        {
            filter.Validate();

            string[] splitedScreenSize = string.IsNullOrEmpty(filter.ss) ? null : filter.ss.Split(new char[] { 'X' });
            int? sw = splitedScreenSize == null ? null : (int?)int.Parse(splitedScreenSize[0]);
            int? sh = splitedScreenSize == null ? null : (int?)int.Parse(splitedScreenSize[1]);
            var filterData = ObjectContainer.Instance.RunQuery(new FilterQuery(
                                filter.fd.Value,
                                filter.td.Value,
                                filter.pid,
                                filter.aid,
                                sh,
                                sw,
                                null,
                                null,
                                null,
                                null,
                                null));

            return View(new FilterModel(), AnalyticsMasterModel.MenuItem.FingerPrint, filterData, filter);
        }

        public ActionResult EyeTracker(FilterParametersModel filter)
        {
            var s = EmailSettings.Settings.Forward;
            filter.Validate();

            string[] splitedScreenSize = string.IsNullOrEmpty(filter.ss) ? null : filter.ss.Split(new char[] { 'X' });
            int? sw = splitedScreenSize == null ? null : (int?)int.Parse(splitedScreenSize[0]);
            int? sh = splitedScreenSize == null ? null : (int?)int.Parse(splitedScreenSize[1]);
            var filterData = ObjectContainer.Instance.RunQuery(new FilterQuery(
                                filter.fd.Value,
                                filter.td.Value,
                                filter.pid,
                                filter.aid,
                                sh,
                                sw,
                                null,
                                null,
                                null,
                                null,
                                null));

            return View(new FilterModel(), AnalyticsMasterModel.MenuItem.EyeTracker, filterData, filter);
        }

        public FileResult ClickHeatMapImage(FilterParametersModel filter)
        {
            string[] splitedScreenSize = filter.ss.Split(new char[] { 'X' });
            int sw = int.Parse(splitedScreenSize[0]);
            int sh = int.Parse(splitedScreenSize[1]);
            byte[] imageData = null;
            var opResult = analyticsService.GetClickHeatMapData(filter.aid.Value, filter.p, sw, sh, filter.fd.Value, filter.td.Value);
            if (!opResult.HasError)
            {
                Image bgImg = GetBackgroundImage(filter.aid.Value, sw, sh);
                Image image = HeatMapImage_.CreateClickHeatMap(opResult.Value, sw, sh, bgImg);
                using (MemoryStream mStream = new MemoryStream())
                {
                    image.Save(mStream, ImageFormat.Png);
                    imageData = mStream.ToArray();
                }
                image.Dispose();

            }
            if (imageData == null)
            {
                throw new HttpException(404, "Not found");
            }
            else
            {
                return base.File(imageData, "Image/png");
            }
        }

        public FileResult ViewHeatMapImage(FilterParametersModel filter)
        {
            string[] splitedScreenSize = filter.ss.Split(new char[] { 'X' });
            int sw = int.Parse(splitedScreenSize[0]);
            int sh = int.Parse(splitedScreenSize[1]);
            byte[] imageData = null;
            var opResult = analyticsService.GetViewHeatMapData(filter.aid.Value, filter.p, sw, sh, filter.fd.Value, filter.td.Value);
            if (!opResult.HasError)
            {
                Image bgImg = GetBackgroundImage(filter.aid.Value, sw, sh);
                Image image = HeatMapImage_.CreateViewHeatMap(opResult.Value, sw, sh, bgImg);
                using (MemoryStream mStream = new MemoryStream())
                {
                    image.Save(mStream, ImageFormat.Png);
                    imageData = mStream.ToArray();
                }
            }
            if (imageData == null)
            {
                throw new HttpException(404, "Not found");
            }
            else
            {
                return base.File(imageData, "Image/png");
            }
        }

        private Image GetBackgroundImage(long appId, int clientWidth, int clientHeight)
        {
            string bgPath = Path.Combine(Server.MapPath("/Users_Resources/Screens"), string.Format("{0}.{1}.{2}.png", appId, clientWidth, clientHeight));
            Image bgImg = null;
            if (System.IO.File.Exists(bgPath)) bgImg = Image.FromFile(bgPath);
            return bgImg;
        }

        /// <summary>
        /// After login page content
        /// </summary>
        /// <param name="urlPart1"></param>
        /// <param name="urlPart2"></param>
        /// <param name="urlPart3"></param>
        /// <returns></returns>
        public ActionResult PageContent(string urlPart1, string urlPart2, string urlPart3)
        {
            string path = urlPart1;
            if (!string.IsNullOrEmpty(urlPart2))
            {
                path += "/" + urlPart2;
            }
            if (!string.IsNullOrEmpty(urlPart3))
            {
                path += "/" + urlPart3;
            }

            var page = ObjectContainer.Instance.RunQuery(new GetPageQuery(path.ToLower()));
            if (page == null)
            {
                return View("404", new ContentModel { }, AfterLoginMasterModel.MenuItem.None);
            }
            else
            {
                AfterLoginMasterModel.MenuItem selectedItem = AfterLoginMasterModel.MenuItem.None;
                if (!Enum.TryParse<AfterLoginMasterModel.MenuItem>(urlPart1, true, out selectedItem))
                {
                    selectedItem = AfterLoginMasterModel.MenuItem.None;
                }
                return View(new ContentModel { Title = page.Title, Content = page.Content }, selectedItem);
            }
        }
    }
}

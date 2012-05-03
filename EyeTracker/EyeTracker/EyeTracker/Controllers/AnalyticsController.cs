using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Core.Services;
using EyeTracker.Helpers;
using EyeTracker.Model;
using EyeTracker.Domain.Model;
using System.Web.Script.Serialization;
using EyeTracker.Core;
using EyeTracker.Common;
using EyeTracker.Domain;
using System.Collections.ObjectModel;
using EyeTracker.Model.Filter;
using EyeTracker.Model.Master;
using EyeTracker.Model.Pages.Analytics;
using EyeTracker.Controllers.Master;
using EyeTracker.Domain.Common;
using EyeTracker.Common.Queries.Analytics;
using System.Drawing;
using EyeTracker.Models;
using System.IO;
using System.Drawing.Imaging;
using EyeTracker.Common.Queries.Analytics.QueryResults;
using EyeTracker.Common.Queries;

namespace EyeTracker.Controllers
{
    [Authorize]
    public class AnalyticsController : FilterController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IPortfolioService portfolioService;
        private IAccountService accountService;
        private IApplicationService applicationService;
        private IAnalyticsService analyticsService;

        public AnalyticsController()
            : this(new PortfolioService(),
            new ApplicationService(),
            new AnalyticsService(),
            new AccountService())
        {
        }

        public AnalyticsController(
            IPortfolioService portfolioService,
            IApplicationService applicationService,
            IAnalyticsService analyticsService,
            IAccountService accountService)
        {
            this.portfolioService = portfolioService;
            this.accountService = accountService;
            this.applicationService = applicationService;
            this.analyticsService = analyticsService;
        }

        public override AfterLoginMasterModel.SelectedMenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.SelectedMenuItem.Analytics; }
        }

        public ActionResult Index()
        {
            var portfolioData = ObjectContainer.Instance.RunQuery(new PortfoliosQuery());
            FilterParametersModel filter = null;
            if(portfolioData.Portfolios1.Any())
            {
                var portfolio = portfolioData.Portfolios1.First();
                filter = new FilterParametersModel
                {
                    pid = portfolio.Id
                };
                filter.Validate();
            }
            return View(new IndexViewModel(portfolioData.Portfolios1), AnalyticsMasterModel.MenuItem.Portfolios, portfolioData, filter);
        }

        public ActionResult Dashboard(FilterParametersModel filter)
        {
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
            var usageViewData = ObjectContainer.Instance.RunQuery(new FingerPrintViewDataQuery(
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
                                null,
                                DataGrouping.Day));
            
            //if (filter.aid.HasValue && !string.IsNullOrEmpty(filter.ss) && !string.IsNullOrEmpty(filter.p))
            //{
            //    imageUrl = string.Format("/Application/ClickHeatMapImage/{0}/?aId={0}&p={1}&sw={2}&sh={3}&fd={4}&td={5}&preview=true",
            //                        filter.aid,
            //                        HttpUtility.UrlEncode(filter.p),
            //                        sw,
            //                        sh,
            //                        HttpUtility.UrlEncode(filter.fd.Value.ToString("HH:mm dd-MMM-yyyy")),
            //                        HttpUtility.UrlEncode(filter.td.Value.ToString("HH:mm dd-MMM-yyyy")));
            //}

            var model = new FingerPrintModel 
            {
                //ImageUrl = imageUrl
            };

            return View(model, AnalyticsMasterModel.MenuItem.FingerPrint, usageViewData, filter);
        }

        public ActionResult EyeTracker(FilterParametersModel filter)
        {
            filter.Validate();

            string[] splitedScreenSize = string.IsNullOrEmpty(filter.ss) ? null : filter.ss.Split(new char[] { 'X' });
            int? sw = splitedScreenSize == null ? null : (int?)int.Parse(splitedScreenSize[0]);
            int? sh = splitedScreenSize == null ? null : (int?)int.Parse(splitedScreenSize[1]);
            var usageViewData = ObjectContainer.Instance.RunQuery(new FingerPrintViewDataQuery(
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
                                null,
                                DataGrouping.Day));
            /*
            var dataResult = analyticsService.GetEyeTrackerData(portfolioId, applicationId, fromDate.Value, toDate.Value);
            if (dataResult.HasError)
            {
                return View("Error");
            }
            */
            var model = new FingerPrintModel
            {
                //ImageUrl = imageUrl
            };

            return View(model, AnalyticsMasterModel.MenuItem.EyeTracker, usageViewData, filter);
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
    }
}

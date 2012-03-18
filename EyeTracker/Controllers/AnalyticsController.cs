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

namespace EyeTracker.Controllers
{
    [Authorize]
    public class AnalyticsController : Master.AnalyticsMasterController
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

        public ActionResult Index()
        {
            var res = analyticsService.GetAllPortfolios();
            if (res.HasError)
            {
                return View("Error");
            }
            return View(new IndexViewModel(res.Value), AnalyticsMasterModel.MenuItem.Portfolios, string.Empty, AfterLoginMasterModel.SelectedMenuItem.Analytics);
        }

        public ActionResult Dashboard(int portfolioId, int? applicationId, DateTime? fromDate, DateTime? toDate, string screenSize, string path, string language, string os, string location)
        {
            if (!fromDate.HasValue)
            {
                fromDate = DateTime.UtcNow.AddDays(-30);
            }

            if (!toDate.HasValue)
            {
                toDate = DateTime.UtcNow;
            }

            var dashboardViewData = ObjectContainer.Instance.RunQuery(new DashboardViewData(fromDate.Value,
                                                                                        toDate.Value,
                                                                                        portfolioId,
                                                                                        applicationId,
                                                                                        null,
                                                                                        null,
                                                                                        null,
                                                                                        null,
                                                                                        null,
                                                                                        null,
                                                                                        null,
                                                                                        DataGrouping.Day));

            //Grouping data by day. To show on graph all days from start till end.
            var visitsData = new List<object[]>();
            int diffDays = (toDate.Value - fromDate.Value).Days;
            for (int i = 0; i < diffDays; i++)
            {
                int count = 0;
                var curDate = fromDate.Value.AddDays(i);
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

            var filterModel = this.GetFilter("Dashboard", fromDate.Value, toDate.Value, dashboardViewData.Portfolios);

            filterModel.PortfolioId = portfolioId;
            filterModel.AppId = applicationId.HasValue ? applicationId.Value : 0;

            var dashboardModel = new DashboardModel
                                {
                                    UsageChartData = new JavaScriptSerializer().Serialize(usageInitData),
                                    FilterModel = filterModel
                                };

            var leftMenuPath = string.Join("/", new string[] { portfolioId.ToString(), filterModel.AppId.ToString(), fromDate.Value.ToString("dd-MMM-yyyy"), toDate.Value.ToString("dd-MMM-yyyy") });

            return View(dashboardModel, AnalyticsMasterModel.MenuItem.Dashboard, leftMenuPath, AfterLoginMasterModel.SelectedMenuItem.Analytics);
        }

        public ActionResult Usage(int portfolioId, int? applicationId, DateTime? fromDate, DateTime? toDate)
        {
            if (!fromDate.HasValue)
            {
                fromDate = DateTime.UtcNow.AddDays(-30);
            }

            if (!toDate.HasValue)
            {
                toDate = DateTime.UtcNow;
            }

            var query = new UsageViewData(
                fromDate.Value,
                toDate.Value,
                portfolioId,
                applicationId,
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
            int diffDays = (toDate.Value - fromDate.Value).Days;
            for (int i = 0; i < diffDays; i++)
            {
                int count = 0;
                var curDate = fromDate.Value.AddDays(i);
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

            var filterModel = this.GetFilter("Usage", fromDate.Value, toDate.Value, usageViewData.Portfolios);
            filterModel.PortfolioId = portfolioId;
            filterModel.AppId = applicationId.HasValue ? applicationId.Value : 0;
            return View(new UsageModel
                        {
                            UsageChartData = new JavaScriptSerializer().Serialize(usageInitData),
                            FilterModel = filterModel
                        },
                        AnalyticsMasterModel.MenuItem.Usage,
                        string.Join("/", new string[] { portfolioId.ToString(), filterModel.AppId.ToString(), fromDate.Value.ToString("dd-MMM-yyyy"), toDate.Value.ToString("dd-MMM-yyyy") }),
                        AfterLoginMasterModel.SelectedMenuItem.Analytics);
        }

        public ActionResult FingerPrint(int portfolioId, int? applicationId, DateTime? fromDate, DateTime? toDate)
        {
            if (!fromDate.HasValue)
            {
                fromDate = DateTime.UtcNow.AddDays(-30);
            }

            if (!toDate.HasValue)
            {
                toDate = DateTime.UtcNow;
            }

            int appId = applicationId.HasValue ? applicationId.Value : 0;

            var query = new FingerPrintViewData(
                fromDate.Value,
                toDate.Value,
                portfolioId,
                applicationId,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                DataGrouping.Day);

            /*
            var res = service.GetEyeTrackerData(appId, fromDate, toDate);
            ViewData["ScreenSizes"] = new List<SelectListItem>(res.Value.ScreenSizes.Select(s => new SelectListItem { Text = string.Format("{0} X {1}", s.Width, s.Height), Value = string.Format("{0}X{1}", s.Width, s.Height) }));
            ViewData["PageUris"] = new List<SelectListItem>(res.Value.PageUris.Select(s => new SelectListItem { Text = s, Value = s }));
            ViewBag.EyeTrackerImageUrl = string.Format("/Application/ClickHeatMapImage/{0}/?appId={0}&pageUri={1}&clientWidth={2}&clientHeight={3}&fromDate={4}&toDate={5}&preview=true", appId, HttpUtility.UrlEncode(res.Value.PageUris.First()), res.Value.ScreenSizes.First().Width, res.Value.ScreenSizes.First().Height, HttpUtility.UrlEncode(fromDate.ToString("HH:mm dd-MMM-yyyy")), HttpUtility.UrlEncode(toDate.ToString("HH:mm dd-MMM-yyyy")));



            var dataResult = analyticsService.GetClickHeatMapData(portfolioId, applicationId, fromDate.Value, toDate.Value);
            if (dataResult.HasError)
            {
                return View("Error");
            }
            */
            return View();
        }

        public ActionResult EyeTracker(int portfolioId, int? applicationId, DateTime? fromDate, DateTime? toDate)
        {
            if (!fromDate.HasValue)
            {
                fromDate = DateTime.UtcNow.AddDays(-30);
            }

            if (!toDate.HasValue)
            {
                toDate = DateTime.UtcNow;
            }

            int appId = applicationId.HasValue ? applicationId.Value : 0;
            /*
            var dataResult = analyticsService.GetEyeTrackerData(portfolioId, applicationId, fromDate.Value, toDate.Value);
            if (dataResult.HasError)
            {
                return View("Error");
            }
            */
            return View();
        }

        private FilterModel GetFilter(string action, DateTime fromDate, DateTime toDate, IEnumerable<PortfolioResult> portfolios)
        {
            var js = new JavaScriptSerializer();
            return new FilterModel
            {
                PortfoliosData = string.Format("{{{0}}}", string.Join(",", portfolios.Select(p => string.Format("{0}:{1}", p.Id, js.Serialize(p.Applications.Select(a => new { id = a.Id, desc = a.Description })))))),
                Portfolios = portfolios.Select(p => new SelectListItem() { Text = p.Description, Value = p.Id.ToString() }),
                FormAction = action,
                Date = new DateModel
                {
                    DateFrom = fromDate,
                    DateTo = toDate,
                },
                ShowDateSelector = true,
                Applications = new SelectorModel
                {
                    Title = "Add Application",
                    Items = Enumerable.Range(8, 30).Select((num, i) => new SelectorItem { Id = num, Text = "App " + num, Index = i }),
                    SelectedItems = new List<SelectorItem>()
                },
                ScreenSizes = new SelectorModel
                {
                    Title = "Screen Sizes",
                    Items = Enumerable.Range(8, 30).Select((num, i) => new SelectorItem { Id = num, Text = "App " + num, Index = i }),
                    SelectedItems = new List<SelectorItem>()
                }
            };
        }


        public FileResult ClickHeatMapImage(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate, bool preview)
        {
            byte[] imageData = null;
            var opResult = analyticsService.GetClickHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
            if (!opResult.HasError)
            {
                Image bgImg = GetBackgroundImage(appId, clientWidth, clientHeight);
                Image image = HeatMapImage_.CreateClickHeatMap(opResult.Value, clientWidth, clientHeight, bgImg);
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

        public FileResult ViewHeatMapImage(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate, bool preview)
        {
            byte[] imageData = null;
            var opResult = analyticsService.GetViewHeatMapData(appId, pageUri, clientWidth, clientHeight, fromDate, toDate);
            if (!opResult.HasError)
            {
                Image bgImg = GetBackgroundImage(appId, clientWidth, clientHeight);
                Image image = HeatMapImage_.CreateViewHeatMap(opResult.Value, clientWidth, clientHeight, bgImg);
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

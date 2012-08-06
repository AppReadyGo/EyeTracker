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
using EyeTracker.Model.Filter;
using EyeTracker.Model.Master;
using EyeTracker.Model.Pages.Analytics;
using EyeTracker.Model.Pages.Home;
using EyeTracker.Models;
using EyeTracker.Common.QueryResults.Analytics;

namespace EyeTracker.Controllers
{
    [Authorize]
    public class AnalyticsController : FilterController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        
        public override AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.MenuItem.Analytics; }
        }

        public ActionResult Dashboard(FilterParametersModel filter)
        {
            log.WriteInformation("Dashboard");
            if (ModelState.IsValid)
            {
                var dashboardViewData = ObjectContainer.Instance.RunQuery(
                            new DashboardViewDataQuery(filter.FromDate,
                                                filter.ToDate,
                                                filter.PortfolioId,
                                                filter.ApplicationId,
                                                filter.ScreenSize,
                                                filter.Path,
                                                filter.Language,
                                                filter.OperationSystem,
                                                filter.Country,
                                                filter.City,
                                                DataGrouping.Day));

                //Grouping data by day. To show on graph all days from start till end.
                var visitsData = new List<object[]>();
                int diffDays = (filter.ToDate - filter.FromDate).Days;
                for (int i = 0; i < diffDays; i++)
                {
                    int count = 0;
                    var curDate = filter.FromDate.AddDays(i);
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
                    ContentOverviewData = dashboardViewData.ContentOverview.Select((d, i) => new ContentOverviewModel { Path = d.Path, Views = d.Views, Index = i }).ToArray()
                };

                dashboardModel.Title = "Dashboard";

                return View(dashboardModel, AnalyticsMasterModel.MenuItem.Dashboard, dashboardViewData, filter, false);
            }
            else
            {
                return Redirect("~/Error");
            }
        }

        public ActionResult Usage(FilterParametersModel filter)
        {
            if (ModelState.IsValid)
            {
                var query = new UsageViewDataQuery(
                filter.FromDate,
                filter.ToDate,
                filter.PortfolioId,
                filter.ApplicationId,
                filter.ScreenSize,
                filter.Path,
                filter.Language,
                filter.OperationSystem,
                filter.Country,
                filter.City,
                DataGrouping.Day);

                var usageViewData = ObjectContainer.Instance.RunQuery(query);

                //Grouping data by day. To show on graph all days from start till end.
                var data = new List<object[]>();
                int diffDays = (filter.ToDate - filter.FromDate).Days;
                for (int i = 0; i < diffDays; i++)
                {
                    int count = 0;
                    var curDate = filter.FromDate.AddDays(i);
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

                return View(model, AnalyticsMasterModel.MenuItem.Usage, usageViewData, filter, false);
            }
            else
            {
                return Redirect("~/Error");
            }
        }

        public ActionResult FingerPrint(FilterParametersModel filter)
        {
            if (ModelState.IsValid)
            {
                var filterData = ObjectContainer.Instance.RunQuery(new FilterQuery(
                                     filter.FromDate,
                                     filter.ToDate,
                                     filter.PortfolioId,
                                     filter.ApplicationId,
                                     filter.ScreenSize,
                                     filter.Path,
                                     null,
                                     null,
                                     null,
                                     null));

                return View(new FilterModel() { Title = "Fingerprint" }, AnalyticsMasterModel.MenuItem.FingerPrint, filterData, filter, true);
            }
            else
            {
                return Redirect("~/Error");
            }
       }

        public ActionResult EyeTracker(FilterParametersModel filter)
        {            if (ModelState.IsValid)
            {

            var filterData = ObjectContainer.Instance.RunQuery(new FilterQuery(
                                filter.FromDate,
                                filter.ToDate,
                                filter.PortfolioId,
                                filter.ApplicationId,
                                filter.ScreenSize,
                                filter.Path,
                                null,
                                null,
                                null,
                                null));

            return View(new FilterModel() { Title = "Eye Tracker" }, AnalyticsMasterModel.MenuItem.EyeTracker, filterData, filter, true);
             }
            else
            {
                return Redirect("~/Error");
            }
       }

        public FileResult ClickHeatMapImage(FilterParametersModel filter)
        {
            var result = ObjectContainer.Instance.RunQuery(new ClickHeatMapDataQuery(filter.ApplicationId.Value, filter.Path, filter.ScreenSize.Value, filter.FromDate, filter.ToDate));
            
            byte[] imageData = null;
            Image image = null;
            if (result.Data.Any())
            {
                Image bgImg = GetBackgroundImage(result.Screen);
                image = HeatMapImage_.CreateClickHeatMap(result.Data, filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height, bgImg);
            }
            else
            {
                //Show no data
                image = HeatMapImage_.CreateEmpityBackground("NO DATA", filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height);
            }

            if (image != null)
            {
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
            var result = ObjectContainer.Instance.RunQuery(new HeatMapDataQuery(filter.ApplicationId.Value, filter.Path, filter.ScreenSize.Value, filter.FromDate, filter.ToDate));
            
            byte[] imageData = null;
            Image image = null;

            if (result.Data.Any())
            {
                Image bgImg = GetBackgroundImage(result.Screen);
                image = HeatMapImage_.CreateViewHeatMap(result.Data, filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height, bgImg);
            }
            else
            {
                //Show no data
                image = HeatMapImage_.CreateEmpityBackground("NO DATA", filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height);
            }

            if (image != null)
            {
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

        private Image GetBackgroundImage(ScreenResult screen)
        {
            Image bgImg = null;
            if (screen != null)
            {
                string bgPath = Path.Combine(Server.MapPath("~/Restricted/Screens/"), string.Concat(screen.Id, screen.FileExtension));
                if (System.IO.File.Exists(bgPath)) bgImg = Image.FromFile(bgPath);
            }
            return bgImg;
        }
    }
}

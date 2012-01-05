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

namespace EyeTracker.Controllers
{
    [Authorize]
    public class AnalyticsController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private IPortfolioService portfolioService;
        private IAccountService accountService;
        private IReportsService reportService;
        private IApplicationService applicationService;
        private IAnalyticsService analyticsService;

        public AnalyticsController()
            : this(new PortfolioService(), new ApplicationService(), new AnalyticsService(), new AccountService(), new ReportsService())
        {
        }

        public AnalyticsController(IPortfolioService portfolioService, IApplicationService applicationService, IAnalyticsService analyticsService, IAccountService accountService, IReportsService reportService)
        {
            this.portfolioService = portfolioService;
            this.accountService = accountService;
            this.reportService = reportService;
            this.applicationService = applicationService;
            this.analyticsService = analyticsService;
        }

        public ActionResult Index()
        {
            var res = analyticsService.GetCurrentUserPortfolios();
            if (res.HasError)
            {
                return View("Error");
            }

            ViewBag.Data = res.Value;

            ViewData["analytics"] = "class=\"selected\"";
            return View();
        }

        public ActionResult Dashboard(AnalyticsType type, int Id, DateTime? fromDate, DateTime? toDate)
        {
            if (!fromDate.HasValue)
            {
                fromDate = DateTime.UtcNow.AddDays(-30);
            }

            if(!toDate.HasValue)
            {
                toDate = DateTime.UtcNow;
            }

            var dataResult = analyticsService.GetDashboardData(type, Id, fromDate.Value, toDate.Value);
            if (dataResult.HasError)
            {
                return View("Error");
            }
            var dashboardData = dataResult.Value;

            var data = new List<object[]>();
            int diffDays = (toDate.Value - fromDate.Value).Days;
            for (int i = 0; i < diffDays; i++)
            {
                int count = 0;
                var curDate = fromDate.Value.AddDays(i);
                if (dashboardData.ViewsData.ContainsKey(curDate))
                {
                    count = dashboardData.ViewsData[curDate];
                }
                data.Add(new object[] { curDate.MilliTimeStamp(), count });
            }

            //Fill chart data
            var usageInitData = new List<object>();
            usageInitData.Add(new
            {
                data = data,
                color = "#461D7C"
            });

            ViewBag.Type = type.ToString();
            ViewBag.UsageInitData = new JavaScriptSerializer().Serialize(usageInitData);
            ViewBag.Id = Id;
            ViewBag.CurrentName = dashboardData.Description;
            ViewBag.AnalyticsType = type;

            BindFilterData(fromDate.Value, toDate.Value);

            var navigationItems = new List<KeyValuePair<string, string>>() 
            { 
                new KeyValuePair<string, string>("/Analytics","Portfolios")
            };

            if (type == AnalyticsType.Application)
            {
                var appDashboardData = (ApplicationDashboardData)dashboardData;
                navigationItems.Add(new KeyValuePair<string, string>(string.Format("/Analytics/{0}/Dashboard/{1}", AnalyticsType.Portfolio, appDashboardData.PortfolioId), appDashboardData.PortfolioDescription));
                navigationItems.Add(new KeyValuePair<string, string>("/Application", "Applications"));
            }
            navigationItems.Add(new KeyValuePair<string, string>(null, dashboardData.Description));

            ViewData["breadCrumbItems"] = navigationItems;

            ViewData["Applications"] = new SelectorModel
            {
                Title = "Application",
                Items = new Dictionary<int, string>() { { 1, "Appl 1" } },
                SelectedItems = new Dictionary<int, string>() { { 1, "Appl 2" } }
            };

            return View();
        }

        public ActionResult Usage(int Id)
        {
            var reportRes = reportService.GetVisitsData(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow, Id, null, DataGrouping.Day);
            if (reportRes.HasError)
            {
                return View("Error");
            }
            //Fill chart data
            var chartInitData = new List<object>();
            chartInitData.Add(new
            {
                data = reportRes.Value.OrderBy(curItem => curItem.Key).Select(curItem => new object[] { curItem.Key.MilliTimeStamp(), curItem.Value }),
                color = "#461D7C"
            });
            ViewBag.ChartInitData = new JavaScriptSerializer().Serialize(chartInitData);
            ViewBag.PortfolioId = Id;
            return View();
        }

        public ActionResult FingerPrint(int id)
        {
            ViewData["analytics"] = "class=\"selected\"";
            return View();
        }

        public ActionResult EyeTracker(int id)
        {
            ViewData["analytics"] = "class=\"selected\"";
            return View();
        }

        private void BindFilterData(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
        }
    }
}

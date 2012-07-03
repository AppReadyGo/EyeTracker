using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EyeTracker.Common.Commands;
using EyeTracker.Common.Entities;
using EyeTracker.Common.Logger;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Core;
using EyeTracker.Core.Services;
using EyeTracker.Domain.Model;
using EyeTracker.Model.Master;
using EyeTracker.Model.Pages.Application;

namespace EyeTracker.Controllers
{
    [Authorize]
    public class ApplicationController : FilterController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public override AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.MenuItem.Analytics; }
        }
         
        public ActionResult New(int id)
        {
            var viewData = GetViewData(id);
            return View(new ApplicationModel { PortfolioId = id, ViewData = viewData }, AnalyticsMasterModel.MenuItem.Portfolios);
        }

        [HttpPost]
        public JsonResult New(ApplicationModel model)
        {
            object res = null;
            if (ModelState.IsValid)
            {
                var appId = ObjectContainer.Instance.Dispatch(new CreateApplicationCommand(model.PortfolioId, model.Description, model.Type));
                res = new
                {
                    HasError = false,
                    code = GetAppKey(model.Type, model.PortfolioId, appId.Result),
                    appId = appId.Result
                };
            }
            else
            {
                res = new { };
            }
            return Json(res);
        }

        public ActionResult Edit(int id)
        {
            var app = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(id));
            if (app == null)
            {
                return View("Error");
            }
            else
            {
                var model = new ApplicationEditModel
                {
                    Id = app.Id,
                    Description = app.Description,
                    Type = app.Type,
                    PortfolioId = app.PortfolioId
                };
                model.ViewData = GetViewData(model.PortfolioId, model.Type, model.Id);

                return View(model, AnalyticsMasterModel.MenuItem.Portfolios);
            }
        }

        [HttpPost]
        public ActionResult Edit(ApplicationEditModel model)
        {
            if (ModelState.IsValid)
            {
                var appId = ObjectContainer.Instance.Dispatch(new UpdateApplicationCommand(model.Id, model.Description));
                return Redirect("/Analytics");
            }
            else
            {
                model.ViewData = GetViewData(model.PortfolioId, model.Type, model.SelectedApplicationId);
                return View(model, AnalyticsMasterModel.MenuItem.Portfolios);
            }
        }

        public ActionResult Remove(int id)
        {
            var appId = ObjectContainer.Instance.Dispatch(new RemoveApplicationCommand(id));
            return Redirect("/Analytics");
        }

        private static ApplicationViewModel GetViewData(int portfolioId, ApplicationType? type = null, int? appId = null)
        {
            return new ApplicationViewModel
            {
                Screens = new List<Screen>(),
                TypesList = Enum.GetValues(typeof(ApplicationType)).Cast<ApplicationType>().Select(i => new SelectListItem() { Text = i.ToString(), Value = ((int)i).ToString() }),
                PropertyId = GetAppKey(type, portfolioId, appId)
            };
        }

        private static string GetAppKey(ApplicationType? type, int? portfolioId, int? applicationId)
        {
            string key = "";
            if (type.HasValue)
            {
                switch (type)
                {
                    case ApplicationType.Android:
                        key = "MA";
                        break;
                    case ApplicationType.Web:
                        key = "WP";
                        break;
                    case ApplicationType.iPhone:
                        key = "MI";
                        break;
                    case ApplicationType.WebMobile:
                        key = "WM";
                        break;
                    case ApplicationType.WindowsMobile:
                        key = "MW";
                        break;
                }
            }

            if (type.HasValue && portfolioId.HasValue && applicationId.HasValue)
            {
                return string.Format("{0}-{1:0000}-{2:000000}", key, portfolioId.Value, applicationId.Value);
            }
            else
            {
                return "**-****-******";
            }
        }

        /*
        public ActionResult Index(int portfolioId)
        {
            var appRes = service.GetAll(portfolioId);
            if (appRes.HasError)
            {
                return View("Error");
            }

            ViewBag.PortfolioId = portfolioId;
            var columnHeaders = new List<HTMLTable.Cell>() {
                    new HTMLTable.Cell() { Value = "Description" }, 
                    new HTMLTable.Cell() { Value = "Type" }, 
                    new HTMLTable.Cell() { Value = "% Change" },
                    new HTMLTable.Cell() { Value = "" } 
                };
            var data = new List<List<HTMLTable.Cell>>();

            if (appRes.Value.Count > 0)
            {
                //Create table
                foreach (var curApp in appRes.Value)
                {
                    var cells = new List<HTMLTable.Cell>();
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"/Analytics/Application/Dashboard/{0}\">{1}</a>", curApp.Id, curApp.Description) });
                    cells.Add(new HTMLTable.Cell() { Value = curApp.Type.ToString() });
                    cells.Add(new HTMLTable.Cell() { Value = "0.00%" });
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"/Application/Edit/{0}/{1}\">edit</a>&nbsp;<a href=\"/Application/Remove/{0}/{1}\">remove</a>", portfolioId, curApp.Id) });
                    data.Add(cells);
                }
            }
            else
            {
                data.Add(new List<HTMLTable.Cell>() { new HTMLTable.Cell() { ColSpan = 8, StyleClass = "no-data", Value = "No Applications" } });
            }

            ViewData["caption"] = new HTMLTable.Cell() { Value = "Accounts" };
            ViewData["columnHeaders"] = columnHeaders;
            ViewData["data"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult AddScreen(ScreenDetailsModel screenDetails)
        {
            object res = null;
            if (ModelState.IsValid)
            {
                var file = Request.Files["screen_img"];
                var screen = new Screen { 
                    ApplicationId = screenDetails.AppId,
                    Width = screenDetails.Width,
                    Height = screenDetails.Height,
                    FileExtension = Path.GetExtension(file.FileName)
                };
                var addRes = service.AddScreen(screen);
                if (!addRes.HasError)
                {
                    string tmpFileFullName = null;
                    if (file.ContentLength > 0)
                    {
                        tmpFileFullName = Path.Combine(HttpContext.Server.MapPath("/Users_Resources/Screens/"), string.Format("{0}_{1}X{2}{3}", screenDetails.AppId, screen.Width, screen.Height, screen.FileExtension));
                        file.SaveAs(tmpFileFullName);
                    }
                    res = new { HasError = false, ScreenId = addRes.Value };
                }
                else
                {
                    res = new { HasError = true, ScreenId = -1 };
                }
            }
            var actionResult = base.Json(res);
            actionResult.ContentType = "text/html";
            return actionResult;
        }

        public FileResult Screen(int appId, int width, int height, string file)
        {
            var res = service.GetScreen(appId, width, height);
            if (!res.HasError && res.Value != null)
            {
                string tmpFileFullName = Path.Combine(HttpContext.Server.MapPath("/Users_Resources/Screens/"), string.Format("{0}_{1}X{2}{3}", res.Value.ApplicationId, res.Value.Width, res.Value.Height, res.Value.FileExtension));
                return base.File(tmpFileFullName, MIMEAssistant.GetMIMEType(tmpFileFullName));
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }

        public ActionResult Dashboard(int portfolioId, int appId)
        {
            var points = new Dictionary<DateTime, int> { { DateTime.Now.AddDays(-5), 40 }, { DateTime.Now.AddDays(-4), 30 }, { DateTime.Now.AddDays(-3), 10 }, { DateTime.Now.AddDays(-2), 50 }, { DateTime.Now.AddDays(-1), 40 } };
            //Fill chart data
            var chartInitData = new List<object>();
            chartInitData.Add(new
            {
                data = points.OrderBy(curItem => curItem.Key).Select(curItem => new object[] { curItem.Key.MilliTimeStamp(), curItem.Value }),
                color = "#461D7C"
            });
            ViewBag.ChartInitData = new JavaScriptSerializer().Serialize(chartInitData);
            ViewBag.PortfolioId = portfolioId;
            ViewBag.ApplicationId = appId;
            return View();
        }

        public ActionResult EyeTracker(int portfolioId, int appId)
        {
            ViewBag.PortfolioId = portfolioId;
            ViewBag.ApplicationId = appId;

            DateTime fromDate = DateTime.UtcNow.AddDays(-30);
            DateTime toDate = DateTime.UtcNow;
            var res = service.GetEyeTrackerData(appId, fromDate, toDate);
            if (res.HasError)
            {
                return View("Error");
            }
            else
            {
                if (res.Value.PageUris.Count() == 0 || res.Value.ScreenSizes.Count() == 0)
                {
                    ViewBag.NoData = true;
                }
                else
                {
                    ViewBag.NoData = false;
                    ViewData["ScreenSizes"] = new List<SelectListItem>(res.Value.ScreenSizes.Select(s => new SelectListItem { Text = string.Format("{0} X {1}", s.Width, s.Height), Value = string.Format("{0}X{1}", s.Width, s.Height) }));
                    ViewData["PageUris"] = new List<SelectListItem>(res.Value.PageUris.Select(s => new SelectListItem { Text = s, Value = s }));
                    ViewBag.EyeTrackerImageUrl = string.Format("/Application/ViewHeatMapImage/{0}/?appId={0}&pageUri={1}&clientWidth={2}&clientHeight={3}&fromDate={4}&toDate={5}&preview=true", appId, HttpUtility.UrlEncode(res.Value.PageUris.First()), res.Value.ScreenSizes.First().Width, res.Value.ScreenSizes.First().Height, HttpUtility.UrlEncode(fromDate.ToString("HH:mm dd-MMM-yyyy")), HttpUtility.UrlEncode(toDate.ToString("HH:mm dd-MMM-yyyy")));
                }
                return View("Image");
            }
        }

        public ActionResult Fingerprint(int portfolioId, int appId)
        {
            ViewBag.PortfolioId = portfolioId;
            ViewBag.ApplicationId = appId;

            DateTime fromDate = DateTime.UtcNow.AddDays(-30);
            DateTime toDate = DateTime.UtcNow;
            var res = service.GetEyeTrackerData(appId, fromDate, toDate);
            if (res.HasError)
            {
                return View("Error");
            }
            else
            {
                if (res.Value.PageUris.Count() == 0 || res.Value.ScreenSizes.Count() == 0)
                {
                    ViewBag.NoData = true;
                }
                else
                {
                    ViewBag.NoData = false;
                    ViewData["ScreenSizes"] = new List<SelectListItem>(res.Value.ScreenSizes.Select(s => new SelectListItem { Text = string.Format("{0} X {1}", s.Width, s.Height), Value = string.Format("{0}X{1}", s.Width, s.Height) }));
                    ViewData["PageUris"] = new List<SelectListItem>(res.Value.PageUris.Select(s => new SelectListItem { Text = s, Value = s }));
                    ViewBag.EyeTrackerImageUrl = string.Format("/Application/ClickHeatMapImage/{0}/?appId={0}&pageUri={1}&clientWidth={2}&clientHeight={3}&fromDate={4}&toDate={5}&preview=true", appId, HttpUtility.UrlEncode(res.Value.PageUris.First()), res.Value.ScreenSizes.First().Width, res.Value.ScreenSizes.First().Height, HttpUtility.UrlEncode(fromDate.ToString("HH:mm dd-MMM-yyyy")), HttpUtility.UrlEncode(toDate.ToString("HH:mm dd-MMM-yyyy")));
                }
                return View("Image");
            }
        }
        */
    }
}

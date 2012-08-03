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
using EyeTracker.Controllers.Master;
using EyeTracker.Common.Queries.Analytics;
using EyeTracker.Common;
using System.Web;
using EyeTracker.Model.Pages.Portfolio;
using EyeTracker.Common.Commands.Application;
using System.IO;
using EyeTracker.Common.Queries.Application;
using System.Configuration;

namespace EyeTracker.Controllers
{
    [Authorize]
    public class ApplicationController : AfterLoginController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        private static string androidPackageVersion = ConfigurationManager.AppSettings["AndroidPackageVersion"];

        public override AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.MenuItem.Analytics; }
        }

        public ActionResult Index(int id, string srch = "", int scol = 1, int cp = 1)
        {
            var data = ObjectContainer.Instance.RunQuery(new GetAllApplicationsQuery(id, srch, cp, 15));
            ViewData["IsAdmin"] = User.IsInRole(StaffRole.Administrator.ToString());

            var searchStrUrlPart = string.IsNullOrEmpty(srch) ? string.Empty : string.Concat("&srch=", HttpUtility.UrlEncode(srch));
            var model = new ApplicationIndexModel
            {
                PortfolioId = data.PortfolioId,
                PortfolioDescription = data.PortfolioDescription,
                IsOnePage = data.TotalPages == 1,
                Count = data.Count,
                PreviousPage = data.CurPage == 1 ? null : (int?)(data.CurPage - 1),
                NextPage = data.CurPage == data.TotalPages ? null : (int?)(data.CurPage + 1),
                TotalPages = data.TotalPages,
                CurPage = data.CurPage,
                SearchStrUrlPart = searchStrUrlPart,
                SearchStr = srch,
                Applications = data.Applications.Select((a, i) => new ApplicationItemModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    IsActive = a.IsActive,
                    IsAlternative = i % 2 != 0,
                    Key = GetAppKey(a.Type, data.PortfolioId, a.Id),
                    Visits = a.Visits
                }).ToArray()
            };
            return View(model, AfterLoginMasterModel.MenuItem.Analytics);
        }
        
        public ActionResult New(int id)
        {
            var portfolio = ObjectContainer.Instance.RunQuery(new GetPortfolioDetailsQuery(id));
            ViewBag.Edit = false;
            ViewBag.PortfolioDescritpion = portfolio.Description;
            ViewBag.Version = androidPackageVersion;
            var viewData = GetViewData(id);
            return View(new ApplicationModel { PortfolioId = id, ViewData = viewData }, AfterLoginMasterModel.MenuItem.Analytics);
        }

        [HttpPost]
        public JsonResult New(ApplicationModel model)
        {
            object res = null;
            if (ModelState.IsValid)
            {
                var appId = ObjectContainer.Instance.Dispatch(new CreateApplicationCommand(model.PortfolioId, model.Description, (ApplicationType)model.Type));
                res = new
                {
                    HasError = false,
                    code = GetAppKey((ApplicationType)model.Type, model.PortfolioId, appId.Result),
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
            ViewBag.Version = androidPackageVersion;
            ViewBag.Edit = true;
            var app = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(id));
            if (app == null)
            {
                return View("Error");
            }
            else
            {
                ViewBag.PortfolioDescription = app.PortfolioDescription;
                var model = new ApplicationModel
                {
                    Id = app.Id,
                    Description = app.Description,
                    Type = (int)app.Type,
                    PortfolioId = app.PortfolioId
                };
                model.ViewData = GetViewData(model.PortfolioId, app.Type, model.Id);

                return View(model, AfterLoginMasterModel.MenuItem.Analytics);
            }
        }

        [HttpPost]
        public ActionResult Edit(ApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                var appId = ObjectContainer.Instance.Dispatch(new UpdateApplicationCommand(model.Id, model.Description));
                return Redirect("/Application/" + model.PortfolioId);
            }
            else
            {
                var portfolio = ObjectContainer.Instance.RunQuery(new GetPortfolioDetailsQuery(model.PortfolioId));
                ViewBag.Edit = true;
                ViewBag.PortfolioDescritpion = portfolio.Description;
                ViewBag.Version = androidPackageVersion;
                model.ViewData = GetViewData(model.PortfolioId, (ApplicationType)model.Type, model.Id);
                return View(model, AfterLoginMasterModel.MenuItem.Analytics);
            }
        }

        public ActionResult Remove(int id)
        {
            var app = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(id));
            if (app == null)
            {
                return View("Error");
            }
            else
            {
                ObjectContainer.Instance.Dispatch(new RemoveApplicationCommand(id));
            }
            return Redirect("/Application/" + app.PortfolioId);
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

        public ActionResult Screens(int id, string srch = "", int scol = 1, int cp = 1, string orderby = "", string order = "")
        {
            var orderBy = string.IsNullOrEmpty(orderby) ? ScreensQuery.OrderByColumn.Path : (ScreensQuery.OrderByColumn)Enum.Parse(typeof(ScreensQuery.OrderByColumn), orderby, true);
            bool asc = string.IsNullOrEmpty(orderby) ? ((orderBy == ScreensQuery.OrderByColumn.Path) ? false : true) : order.Equals("asc", StringComparison.OrdinalIgnoreCase);
            var data = ObjectContainer.Instance.RunQuery(new ScreensQuery(id, srch, cp, 10, orderBy, asc));
            var searchStrUrlPart = string.IsNullOrEmpty(srch) ? string.Empty : string.Concat("&srch=", HttpUtility.UrlEncode(srch));

            var model = new ScreensListModel
            {
                IsOnePage = data.TotalPages == 1,
                Count = data.Count,
                PreviousPage = data.CurPage == 1 ? null : (int?)(data.CurPage - 1),
                NextPage = data.CurPage == data.TotalPages ? null : (int?)(data.CurPage + 1),
                TotalPages = data.TotalPages,
                CurPage = data.CurPage,
                UrlPart = string.Concat(searchStrUrlPart, string.IsNullOrEmpty(orderby) ? string.Empty : string.Concat("&orderby=", orderby), string.IsNullOrEmpty(order) ? string.Empty : string.Concat("&order=", order)),

                PathOrder = orderBy == ScreensQuery.OrderByColumn.Path && asc ? "desc" : "asc",
                WidthOrder = orderBy == ScreensQuery.OrderByColumn.Width && asc ? "desc" : "asc",
                HeightOrder = orderBy == ScreensQuery.OrderByColumn.Height && asc ? "desc" : "asc",

                SearchStrUrlPart = searchStrUrlPart,
                SearchStr = srch,

                ApplicationId = data.ApplicationId,
                ApplicationDescription = data.ApplicationDescription,
                PortfolioId = data.PortfolioId,
                PortfolioDescription = data.PortfolioDescription,
                Screens = data.Screens.Select((s, i) => new ScreenItemModel
                {
                    Id = s.Id,
                    Width = s.Width,
                    Height = s.Height,
                    Path = s.Path,
                    FileExtention = s.FileExtension,
                    IsAlternative = i % 2 != 0
                }).ToArray()
            };
            return View(model, AfterLoginMasterModel.MenuItem.Analytics);
        }

        public ActionResult ScreenNew(int id)
        {
            var application = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(id));
            ViewBag.PortfolioId = application.PortfolioId;
            ViewBag.PortfolioDescription = application.PortfolioDescription;
            ViewBag.ApplicationDescription = application.Description;
            return View(new ScreenModel { ApplicationId = id }, AfterLoginMasterModel.MenuItem.Analytics);
        }

        [HttpPost]
        public ActionResult ScreenNew(ScreenModel model)
        {
            if (model.Height <= 0)
            {
                ModelState.AddModelError("Height", "Please enter correct height.");
            }
            if (model.Width <= 0)
            {
                ModelState.AddModelError("Width", "Please enter correct width.");
            }
            if(Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("file", "The File field is required.");
            }
            if (ModelState.IsValid)
            {
                string fileExtention = Path.GetExtension(Request.Files[0].FileName);

                var result = ObjectContainer.Instance.Dispatch(new AddScreenCommand(model.ApplicationId, model.Path, model.Width, model.Height, fileExtention));
                if (result.Validation.Any())
                {
                    return View("Error");
                }
                else
                {
                    var path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), result.Result + fileExtention);
                    Request.Files[0].SaveAs(path);
                }
                return Redirect("/Application/Screens/" + model.ApplicationId);
            }
            else
            {
                var application = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(model.ApplicationId));
                ViewBag.PortfolioId = application.PortfolioId;
                ViewBag.PortfolioDescription = application.PortfolioDescription;
                ViewBag.ApplicationDescription = application.Description;

                return View(model, AfterLoginMasterModel.MenuItem.Analytics);
            }
        }

        public ActionResult ScreenEdit(int id)
        {
            var screen = ObjectContainer.Instance.RunQuery(new GetScreenDetailsQuery(id));
            if (screen == null)
            {
                return View("Error");
            }
            else
            {
                ViewBag.PortfolioId = screen.PortfolioId;
                ViewBag.PortfolioDescription = screen.PortfolioDescription;
                ViewBag.ApplicationDescription = screen.ApplicationDescription;

                var model = new ScreenModel
                {
                    Id = screen.Id,
                    Path = screen.Path,
                    Width = screen.Width,
                    Height = screen.Height,
                    FileExtention = screen.FileExtention,
                    ApplicationId = screen.ApplicationId
                };
                return View(model, AfterLoginMasterModel.MenuItem.Analytics);
            }
        }

        [HttpPost]
        public ActionResult ScreenEdit(ScreenModel model)
        {
            if (model.Height <= 0)
            {
                ModelState.AddModelError("Height", "Please enter correct height.");
            }
            if (model.Width <= 0)
            {
                ModelState.AddModelError("Width", "Please enter correct width.");
            }
            if (ModelState.IsValid)
            {
                bool newFile = Request.Files.Count == 1 && Request.Files[0].ContentLength > 0;
                string fileExtention = newFile ? Path.GetExtension(Request.Files[0].FileName) : model.FileExtention;

                var result = ObjectContainer.Instance.Dispatch(new UpdateScreenCommand(model.Id, model.Path, model.Width, model.Height, fileExtention));
                if (result.Validation.Any())
                {
                    return View("Error");
                }
                else
                {
                    if (newFile)
                    {
                        //Remove previous file
                        var path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), model.Id + model.FileExtention);
                        System.IO.File.Delete(path);

                        path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), model.Id + fileExtention);
                        Request.Files[0].SaveAs(path);
                    }
                }
                return Redirect("/Application/Screens/" + model.ApplicationId.ToString());
            }
            else
            {
                var application = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(model.ApplicationId));
                ViewBag.PortfolioId = application.PortfolioId;
                ViewBag.PortfolioDescription = application.PortfolioDescription;
                ViewBag.ApplicationDescription = application.Description;
                return View(model, AfterLoginMasterModel.MenuItem.Analytics);
            }
        }

        public ActionResult ScreenRemove(int id)
        {
            var screen = ObjectContainer.Instance.RunQuery(new GetScreenDetailsQuery(id));
            if (screen == null)
            {
                return View("Error");
            }
            else
            {
                ObjectContainer.Instance.Dispatch(new RemoveScreenCommand(id));
                var path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), screen.Id + screen.FileExtention);
                System.IO.File.Delete(path);
            }
            return Redirect("/Application/Screens/" + screen.ApplicationId);
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

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
using EyeTracker.Common;
using System.Collections.ObjectModel;
using EyeTracker.Controllers.Master;
using EyeTracker.Model.Pages.Portfolio;
using EyeTracker.Model.Master;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using EyeTracker.Model.Pages.Analytics;
using EyeTracker.Common.Commands;
using EyeTracker.Core;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common.Queries.Analytics;

namespace EyeTracker.Controllers
{
    [Authorize]
    public class PortfolioController : AfterLoginController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public override AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.MenuItem.Analytics; }
        }

        public ActionResult Index(string srch = "", int scol = 1, int cp = 1)
        {
            var data = ObjectContainer.Instance.RunQuery(new PortfoliosQuery(srch, cp, 15));
            ViewData["IsAdmin"] = User.IsInRole(StaffRole.Administrator.ToString());

            var rnd = new Random();

            var searchStrUrlPart = string.IsNullOrEmpty(srch) ? string.Empty : string.Concat("&srch=", HttpUtility.UrlEncode(srch));
            var model = new PortfolioIndexModelTmp(this, AfterLoginMasterModel.MenuItem.Analytics)
            {
                IsOnePage = data.TotalPages == 1,
                Count = data.Count,
                PreviousPage = data.CurPage == 1 ? null : (int?)(data.CurPage - 1),
                NextPage = data.CurPage == data.TotalPages ? null : (int?)(data.CurPage + 1),
                TotalPages = data.TotalPages,
                CurPage = data.CurPage,
                SearchStrUrlPart = searchStrUrlPart,
                SearchStr = srch,
                Portfolios = data.Portfolios.Select((p, i) => new PortfolioItemModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    IsActive = p.IsActive,
                    Alternate = i % 2 != 0,
                    Visits = p.Visits,
                    Applications = p.Applications.Select((a, aIndx) => new ApplicationItemModel
                    {
                        Id = a.Id,
                        Description = a.Description,
                        IsActive = a.IsActive,
                        Alternate = aIndx % 2 != 0,
                        Visits = a.Visits,
                        Key = a.Type.GetAppKey(p.Id,a.Id),
                        Downloads = rnd.Next(100),
                        Published = DateTime.Now.AddDays(-rnd.Next(100)).ToString("dd MMM yyyy"),
                        Scrolls = rnd.Next(1000),
                        Clicks = rnd.Next(1000),
                        Time = rnd.Next(100),
                        TargetGroup = rnd.Next(100) > 50 ? "Men 18+" : "Women 18+"
                    }).ToArray()
                }).ToArray(),
                TopApplications = data.TopApplications.Select((a,i) => new TopApplicationsItemModel
                {
                    IsAlternative = i % 2 != 0,
                    Id = a.Id,
                    PortfolioId = a.PortfolioId,
                    Description = a.Description
                }).ToArray(),
                TopScreens = data.TopScreens.Select((s, i) => new TopScreensItemModel
                {
                    IsAlternative = i % 2 != 0,
                    PortfolioId = s.PortfolioId,
                    ApplicationId = s.ApplicationId,
                    ScreenSize = s.ScreenSize.ToFormatedString(),
                    Path = s.Path
                }).ToArray()
            };
            return View("~/Views/Portfolio/Index.cshtml", model, "Tmp");
        }

        public ActionResult New()
        {
            var model = this.GetModel();
            model.TimeZone = 0;
            return View(model, AfterLoginMasterModel.MenuItem.Analytics);
        }

        [HttpPost]
        public ActionResult New(PortfolioModel model)
        {
            if (ModelState.IsValid)
            {
                var res = ObjectContainer.Instance.Dispatch(new CreatePortfolioCommand(model.Description, model.TimeZone));
                return Redirect("/Portfolio");
            }
            else
            {
                return View(this.GetModel(model), AfterLoginMasterModel.MenuItem.Analytics);
            }
        }

        public ActionResult Edit(int id)
        {
            var portfolio = ObjectContainer.Instance.RunQuery(new GetPortfolioDetailsQuery(id));
            if (portfolio == null)
            {
                return View("Error");
            }
            else
            {
                var model = this.GetModel();
                model.Id = portfolio.Id;
                model.Description = portfolio.Description;
                model.TimeZone = portfolio.TimeZone;

                return View(model, AfterLoginMasterModel.MenuItem.Analytics);
            }
        }

        [HttpPost]
        public ActionResult Edit(PortfolioModel model)
        {
            if (ModelState.IsValid)
            {
                var res = ObjectContainer.Instance.Dispatch(new UpdatePortfolioCommand(model.Id, model.Description, model.TimeZone));
                return Redirect("/Portfolio");
            }
            else
            {
                return View(this.GetModel(model), AfterLoginMasterModel.MenuItem.Analytics);
            }
        }

        public ActionResult Remove(int id)
        {
            var res = ObjectContainer.Instance.Dispatch(new RemovePortfolioCommand(id));
            return Redirect("/Portfolio");
        }

        private PortfolioModel GetModel(PortfolioModel model = null)
        {
            var m = model == null ? new PortfolioModel() : model;

            var timeZones = TimeZoneInfo.GetSystemTimeZones().Select((curItem, i) => new { DisplayName = curItem.DisplayName, Id = (short)curItem.BaseUtcOffset.Hours, i = i });
            m.ViewData = new SelectList(timeZones, "Id", "DisplayName");

            return m;
        }
    }
}

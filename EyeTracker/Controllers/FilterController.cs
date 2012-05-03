using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Model.Filter;
using System.Web.Script.Serialization;
using EyeTracker.Common.Queries.Analytics.QueryResults;
using EyeTracker.Model.Pages.Analytics;
using EyeTracker.Model;

namespace EyeTracker.Controllers
{
    public abstract class FilterController : Master.AnalyticsMasterController
    {
        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, FilterDataResult filterDataResult, FilterParametersModel filter)
            where TViewModel : FilterModel
        {
            FillFilter(viewModel, leftMenuSelectedItem, filterDataResult, filter);

            return View(viewModel, leftMenuSelectedItem, GetUrlPart(viewModel));
        }

        private string GetUrlPart(FilterModel filter)
        {
            if (filter.NoData)
            {
                return null;
            }
            else
            {
                var parts = new List<string>(){string.Format("pid={0}",filter.PortfolioId)};
                if (filter.AppId != 0) parts.Add(string.Format("aid={0}", filter.AppId));
                if (!string.IsNullOrEmpty(filter.ScreenSize)) parts.Add(string.Format("ss={0}", filter.ScreenSize));
                if (!string.IsNullOrEmpty(filter.Path)) parts.Add(string.Format("p={0}", HttpUtility.UrlEncode(filter.Path)));
                parts.Add(string.Format("fd={0}", filter.DateFrom.ToString("dd-MMM-yyyy")));
                parts.Add(string.Format("td={0}", filter.DateTo.ToString("dd-MMM-yyyy")));
                return "?" + string.Join("&", parts.ToArray());
            }
        }

        private void FillFilter(FilterModel filterModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, FilterDataResult filterDataResult, FilterParametersModel filter)
        {
            if (filter != null && filterDataResult != null)
            {
                bool isSpecificFilter = leftMenuSelectedItem == AnalyticsMasterModel.MenuItem.EyeTracker || leftMenuSelectedItem == AnalyticsMasterModel.MenuItem.FingerPrint;

                var curPortfolio = filterDataResult.Portfolios.Single(p => p.Id == filter.pid);

                filterModel.PortfolioId = curPortfolio.Id;
                filterModel.PortfolioName = curPortfolio.Description;

                filterModel.DateFrom = filter.fd.Value;
                filterModel.DateTo = filter.td.Value;

                var js = new JavaScriptSerializer();
                filterModel.PortfoliosData = string.Format("{{{0}}}", string.Join(",", filterDataResult.Portfolios.Select(p => string.Format("{0}:{1}", p.Id, js.Serialize(p.Applications.Select(a => new { id = a.Id, desc = a.Description }))))));
                filterModel.ApplicationsData = string.Format("{{{0}}}", string.Join(",", filterDataResult.Portfolios.SelectMany(p => p.Applications).Select(a => string.Format("{0}:{1}", a.Id, js.Serialize(new { scr = a.Screens, pth = a.Pathes })))));
                filterModel.Portfolios = filterDataResult.Portfolios.Select(p => new SelectListItem() { Text = p.Description, Value = p.Id.ToString(), Selected = p.Id == filter.pid });
                filterModel.FormAction = leftMenuSelectedItem.ToString();

                var apps = new List<SelectListItem>();
                var sizes = new List<SelectListItem>();
                var pathes = new List<SelectListItem>();

                //Set current application
                if (!curPortfolio.Applications.Any() || !curPortfolio.Applications.SelectMany(a => a.Screens).Any())
                {
                    filterModel.NoData = true;
                }
                else
                {
                    var curApplication = filter.aid.HasValue ? curPortfolio.Applications.Single(a => a.Id == filter.aid.Value) :
                                                               (isSpecificFilter ? curPortfolio.Applications.First() : null);

                    filterModel.AppId = curApplication != null ? curApplication.Id : 0;
                    if (filterModel.AppId == 0)
                    {
                        filterModel.ApplicationName = "All";
                    }
                    else
                    {
                        filterModel.ApplicationName = curApplication.Description;
                    }

                    if (!isSpecificFilter)
                    {
                        apps.Add(new SelectListItem { Value = "0", Text = "All Applications", Selected = filterModel.AppId == 0 });
                        sizes.Add(new SelectListItem { Value = "0", Text = "All Sizes", Selected = filterModel.ScreenSize == null });
                        pathes.Add(new SelectListItem { Value = "0", Text = "All Pathes", Selected = filterModel.Path == null });
                    }
                    apps.AddRange(curPortfolio.Applications.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Description, Selected = a.Id == filterModel.AppId }));
                    if (curApplication != null)
                    {
                        filterModel.ScreenSize = string.IsNullOrEmpty(filter.ss) ? (isSpecificFilter ? curApplication.Screens.First() : null) : filter.ss;
                        filterModel.Path = string.IsNullOrEmpty(filter.p) ? (isSpecificFilter ? curApplication.Pathes.First() : null) : filter.p;
                        
                        sizes.AddRange(curApplication.Screens.Select(s => new SelectListItem { Value = s, Text = s, Selected = s == filterModel.ScreenSize }));
                        pathes.AddRange(curApplication.Pathes.Select(p => new SelectListItem { Value = p, Text = p, Selected = p == filterModel.Path }));
                    }
                }

                filterModel.Applications = apps;
                filterModel.ScreenSizes = sizes;
                filterModel.Pathes = pathes;
            }
        }
    }
}

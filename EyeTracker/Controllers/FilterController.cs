using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Model.Filter;
using System.Web.Script.Serialization;
using EyeTracker.Model.Pages.Analytics;
using EyeTracker.Model;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using EyeTracker.Common;

namespace EyeTracker.Controllers
{
    public abstract class FilterController : Master.AnalyticsMasterController
    {
        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, FilterDataResult filterDataResult, FilterParametersModel filter, bool isSingleMode, string placeHolderHTML = null)
            where TViewModel : FilterModel
        {
            FillFilter(viewModel, leftMenuSelectedItem, filterDataResult, filter, isSingleMode, placeHolderHTML);

            return View(viewModel, leftMenuSelectedItem, viewModel.GetUrlPart(), filter.ApplicationId.HasValue ? (int?)filter.PortfolioId : null);
        }

        //protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem)
        //    where TViewModel : FilterModel
        //{
        //    viewModel.NoData = true;
        //    return View(viewModel, leftMenuSelectedItem, GetUrlPart(viewModel));
        //}

        

        private void FillFilter(FilterModel filterModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, FilterDataResult filterDataResult, FilterParametersModel filter, bool isSingleMode, string placeHolderHTML)
        {
            if (filter != null && filterDataResult != null)
            {
                var curPortfolio = filterDataResult.Portfolios.Single(p => p.Id == filter.PortfolioId);

                filterModel.PlaceHolderHTML = placeHolderHTML;

                filterModel.SelectedPortfolioId = curPortfolio.Id;
                filterModel.PortfolioName = curPortfolio.Description;

                filterModel.IsSingleMode = isSingleMode;

                filterModel.SelectedDateFrom = filter.FromDate;
                filterModel.SelectedDateTo = filter.ToDate;

                var js = new JavaScriptSerializer();
                filterModel.PortfoliosData = string.Format("{{{0}}}", string.Join(",", filterDataResult.Portfolios.Select(p => string.Format("{0}:{1}", p.Id, js.Serialize(p.Applications.Select(a => new { id = a.Id, desc = a.Description }))))));
                filterModel.ApplicationsData = string.Format("{{{0}}}", string.Join(",", filterDataResult.Portfolios.SelectMany(p => p.Applications).Select(a => string.Format("{0}:{1}", a.Id, js.Serialize(new { scr = a.ScreenSizes.Select(s => s.ToFormatedString()), pth = a.Pathes.Select(p => Server.UrlEncode(p)) })))));
                filterModel.Portfolios = filterDataResult.Portfolios.Select(p => new SelectListItem() { Text = p.Description, Value = p.Id.ToString(), Selected = p.Id == filter.PortfolioId });
                filterModel.FormAction = leftMenuSelectedItem.ToString();

                var apps = new List<SelectListItem>();
                var sizes = new List<SelectListItem>();
                var pathes = new List<SelectListItem>();

                //Set current application
                if (!curPortfolio.Applications.Any())
                {
                    filterModel.NoData = true;
                }
                else
                {
                    filterModel.NoData =  !curPortfolio.Applications.SelectMany(a => a.ScreenSizes).Any();

                    var curApplication = filter.ApplicationId.HasValue ? curPortfolio.Applications.Single(a => a.Id == filter.ApplicationId.Value)
                                                                       : (isSingleMode ? curPortfolio.Applications.First() : null);

                    filterModel.SelectedApplicationId = curApplication != null ? curApplication.Id : 0;

                    if (filterModel.SelectedApplicationId == 0)
                    {
                        filterModel.ApplicationName = "All";
                    }
                    else
                    {
                        filterModel.ApplicationName = curApplication.Description;
                    }

                    apps.AddRange(curPortfolio.Applications.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Description, Selected = a.Id == filterModel.SelectedApplicationId }));
                    
                    if (curApplication != null)
                    {
                        filterModel.SelectedScreenSize = filter.ScreenSize.HasValue ? filter.ScreenSize.Value.ToFormatedString() : (isSingleMode ? curApplication.ScreenSizes.First().ToFormatedString() : null);
                        filterModel.SelectedPath = string.IsNullOrEmpty(filter.Path) ? (isSingleMode ? curApplication.Pathes.First() : null) : filter.Path;

                        sizes.AddRange(curApplication.ScreenSizes.Select(s => new SelectListItem { Value = s.ToFormatedString(), Text = s.ToFormatedString(), Selected = s.ToFormatedString() == filterModel.SelectedScreenSize }));
                        pathes.AddRange(curApplication.Pathes.Select(p => new SelectListItem { Value = p, Text = p, Selected = p == filterModel.SelectedPath }));
                    }
                    else
                    {
                        filterModel.ScreenSizes = new SelectListItem[0];
                        filterModel.Pathes = new SelectListItem[0];
                    }

                    if (!isSingleMode)
                    {
                        apps.Insert(0, new SelectListItem { Value = "", Text = "All Applications", Selected = filterModel.SelectedApplicationId == 0 });
                        sizes.Insert(0, new SelectListItem { Value = "", Text = "All Sizes", Selected = filterModel.SelectedScreenSize == null });
                        pathes.Insert(0, new SelectListItem { Value = "", Text = "All Pathes", Selected = filterModel.SelectedPath == null });
                    }

                    filterModel.Applications = apps;
                    filterModel.ScreenSizes = sizes;
                    filterModel.Pathes = pathes;
                }
            }
        }
    }
}

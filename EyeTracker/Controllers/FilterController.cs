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

namespace EyeTracker.Controllers
{
    public abstract class FilterController : Master.AnalyticsMasterController
    {
        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, FilterDataResult filterDataResult, FilterParametersModel filter, bool isSingleMode)
            where TViewModel : FilterModel
        {
            FillFilter(viewModel, leftMenuSelectedItem, filterDataResult, filter, isSingleMode);

            return View(viewModel, leftMenuSelectedItem, GetUrlPart(viewModel));
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem)
            where TViewModel : FilterModel
        {
            viewModel.NoData = true;
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
                var parts = new List<string>(){string.Format("pid={0}",filter.SelectedPortfolioId)};
                if (filter.SelectedApplicationId.HasValue) parts.Add(string.Format("aid={0}", filter.SelectedApplicationId));
                if (!string.IsNullOrEmpty(filter.SelectedScreenSize)) parts.Add(string.Format("ss={0}", filter.SelectedScreenSize));
                if (!string.IsNullOrEmpty(filter.SelectedPath)) parts.Add(string.Format("p={0}", HttpUtility.UrlEncode(filter.SelectedPath)));
                parts.Add(string.Format("fd={0}", filter.SelectedDateFrom.ToString("dd-MMM-yyyy")));
                parts.Add(string.Format("td={0}", filter.SelectedDateTo.ToString("dd-MMM-yyyy")));
                return "?" + string.Join("&", parts.ToArray());
            }
        }

        private void FillFilter(FilterModel filterModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, FilterDataResult filterDataResult, FilterParametersModel filter, bool isSingleMode)
        {
            if (filter != null && filterDataResult != null)
            {
                var curPortfolio = filterDataResult.Portfolios.Single(p => p.Id == filter.Portfolio);

                filterModel.SelectedPortfolioId = curPortfolio.Id;

                filterModel.IsSingleMode = isSingleMode;

                filterModel.SelectedDateFrom = filter.FromDate;
                filterModel.SelectedDateTo = filter.ToDate;

                // Fill selectors data
                // TODO: redesign it, load data by AJAX
                var js = new JavaScriptSerializer();
                filterModel.PortfoliosData = string.Format("{{{0}}}", string.Join(",", filterDataResult.Portfolios.Select(p => string.Format("{0}:{1}", p.Id, js.Serialize(p.Applications.Select(a => new { id = a.Id, desc = a.Description }))))));
                filterModel.ApplicationsData = string.Format("{{{0}}}", string.Join(",", filterDataResult.Portfolios.SelectMany(p => p.Applications).Select(a => string.Format("{0}:{1}", a.Id, js.Serialize(new { scr = a.ScreenSizes, pth = a.Pathes.Select(p => Server.UrlEncode(p)) })))));
                
                
                filterModel.Portfolios = filterDataResult.Portfolios.Select(p => new SelectListItem() { Text = p.Description, Value = p.Id.ToString(), Selected = p.Id == filter.Portfolio });
                
                filterModel.FormAction = leftMenuSelectedItem.ToString();

                // Create selectors items
                if (!curPortfolio.Applications.Any() || !curPortfolio.Applications.SelectMany(a => a.ScreenSizes).Any())
                {
                    filterModel.NoData = true;
                }
                else
                {
                    IEnumerable<ApplicationResult> curApplications = null;
                    if (isSingleMode)
                    {
                        curApplications = filter.ApplicationId.HasValue ? curPortfolio.Applications.Where(a => a.Id == filter.ApplicationId.Value)
                                                                    : new ApplicationResult[] { curPortfolio.Applications.First() };
                    }
                    else
                    {
                        curApplications = filter.ApplicationId.Any() ? curPortfolio.Applications.Where(a => filter.ApplicationId.Contains(a.Id))
                                                                    : new ApplicationResult[0];

                    }

                    filterModel.SelectedApplicationId = curApplications.Select(a => a.Id);

                    filterModel.Applications = curPortfolio.Applications.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Description, Selected = filterModel.SelectedApplicationId.Contains(a.Id) });

                    if (curApplications.Any())
                    {
                        if (filter.ScreenSize.Any())
                        {
                            filterModel.SelectedScreenSize = isSingleMode ? curApplications.SelectMany(a => a.ScreenSizes).Where(s => filter.ScreenSize.Any(x => s.Height).Select(s => string.Format("{0}X{1}", s.Width, s.Height)).First() : 
                                                                                ;
                        }


                        filterModel.SelectedScreenSize = filter.ScreenSize.Any() ? 
                                                                (isSingleMode ? curApplications.SelectMany(a => a.ScreenSizes).Select(s => string.Format("{0}X{1}", s.Width, s.Height)).First() : null) : filter.ss;
                        filterModel.SelectedPath = string.IsNullOrEmpty(filter.p) ? (isSpecificFilter ? curApplications.Pathes.First() : null) : filter.p;

                        sizes.AddRange(curApplications.Screens.Select(s => new SelectListItem { Value = s, Text = s, Selected = s == filterModel.ScreenSize }));
                        pathes.AddRange(curApplications.Pathes.Select(p => new SelectListItem { Value = p, Text = p, Selected = p == filterModel.Path }));
                    }
                    else
                    {
                        filterModel.ScreenSizes = new SelectListItem[0];
                        filterModel.Pathes = new SelectListItem[0];
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Model;

namespace EyeTracker.Controllers.Master
{
    public abstract class AnalyticsMasterController : AfterLoginController
    {
        /*
        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, string filterUrlPart, AfterLoginMasterModel.MenuItem selectedItem)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, TViewModel>(GetModel(selectedItem), new AnalyticsMasterModel(leftMenuSelectedItem, filterUrlPart, ), viewModel);

            return base.View(model);
        }
         */

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, string filterUrlPart, int? portfolioId)
        {
            return View(new AnalyticsMasterModel(leftMenuSelectedItem, filterUrlPart, portfolioId), viewModel);
        }
    }
}

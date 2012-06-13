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
        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, string filterUrlPart, AfterLoginMasterModel.MenuItem selectedItem)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, TViewModel>(GetModel(selectedItem), new AnalyticsMasterModel(leftMenuSelectedItem, filterUrlPart), viewModel);

            return base.View(model);
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, string filterUrlPart)
        {
            return View(new AnalyticsMasterModel(leftMenuSelectedItem, filterUrlPart), viewModel);
        }
        
        //SubMasterViewModelWrapper<AfterLoginViewModel, AnalyticsModel>
        /* 
        protected ActionResult View(object viewModel)
        {
            var model = ViewModelSubMaterWrapper<AfterLoginViewModel, AnalyticsModel>();
        }

        protected virtual void HttpGetAttribute()
        {
            return null;
        }
       
        protected virtual object CreateModel(object model, TMasterViewModel masterModel)
        {
            var modelType = typeof(object);

            if (model != null)
                modelType = model.GetType();

            var types = new[] { typeof(TMasterViewModel), modelType };
            Type generic = typeof(ViewModelWrapper<,>).MakeGenericType(types);

            return Activator.CreateInstance(generic, masterModel, model);
        }*/
    }
}

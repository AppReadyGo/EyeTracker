using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Model;

namespace EyeTracker.Controllers.Master
{
    [Authorize]
    public abstract class AfterLoginController : Controller
    {
        public abstract AfterLoginMasterModel.SelectedMenuItem SelectedMenuItem { get; }

        protected virtual AfterLoginMasterModel GetModel(AfterLoginMasterModel.SelectedMenuItem selectedItem)
        {
            var model = new AfterLoginMasterModel(selectedItem);
            model.IsAdmin = true;
            return model;
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, TViewModel>(GetModel(SelectedMenuItem), viewModel);
          
            return base.View(model);
        }

        protected virtual ActionResult View<TTemplateModel, TViewModel>(TTemplateModel templateModel, TViewModel viewModel)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, TTemplateModel, TViewModel>(GetModel(SelectedMenuItem), templateModel, viewModel);

            return base.View(model);
        }
    }
}

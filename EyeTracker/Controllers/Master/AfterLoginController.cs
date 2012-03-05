using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Model;

namespace EyeTracker.Controllers.Master
{
    public class AfterLoginController : Controller
    {
        protected virtual AfterLoginMasterModel GetModel(AfterLoginMasterModel.SelectedMenuItem selectedItem)
        {
            return new AfterLoginMasterModel(selectedItem);
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AfterLoginMasterModel.SelectedMenuItem selectedItem)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, TViewModel>(GetModel(selectedItem), viewModel);

            return base.View(model);
        }
    }
}

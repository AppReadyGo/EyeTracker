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
        protected virtual AfterLoginViewModel GetModel(AfterLoginViewModel.SelectedMenuItem selectedItem)
        {
            return new AfterLoginViewModel(selectedItem);
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AfterLoginViewModel.SelectedMenuItem selectedItem)
        {
            var model = new ViewModelWrapper<AfterLoginViewModel, TViewModel>(GetModel(selectedItem), viewModel);

            return base.View(model);
        }
    }
}

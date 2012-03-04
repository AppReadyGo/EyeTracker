using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Model;

namespace EyeTracker.Controllers.Master
{
    public class BeforeLoginController : Controller
    {
        protected virtual BeforeLoginViewModel GetModel(BeforeLoginViewModel.SelectedMenuItem selectedItem)
        {
            return new BeforeLoginViewModel(selectedItem);
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, BeforeLoginViewModel.SelectedMenuItem selectedItem)
        {
            var model = new ViewModelWrapper<BeforeLoginViewModel, TViewModel>(GetModel(selectedItem), viewModel);

            return base.View(model);
        }
    }
}

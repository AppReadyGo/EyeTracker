using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Model;

namespace EyeTracker.Controllers.Master
{
    public class BeforeLoginController : MainController
    {
        protected virtual BeforeLoginMasterModel GetModel(BeforeLoginMasterModel.MenuItem selectedItem)
        {
            return new BeforeLoginMasterModel(selectedItem);
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, BeforeLoginMasterModel.MenuItem selectedItem)
        {
            return base.View(GetModel(selectedItem), viewModel);
        }

        protected virtual ActionResult View<TViewModel>(string viewName, TViewModel viewModel, BeforeLoginMasterModel.MenuItem selectedItem)
        {
            var model = new ViewModelWrapper<BeforeLoginMasterModel, TViewModel>(GetModel(selectedItem), viewModel);

            return base.View(viewName, model);
        }
    }

    public class MainController : Controller
    {
        protected virtual ActionResult View<TSubMaster, TViewModel>(TSubMaster subMater,TViewModel viewModel)
        {
            var model = new ViewModelWrapper<MainMasterModel, TSubMaster, TViewModel>(new MainMasterModel(), subMater, viewModel);

            return base.View(model);
        }
    }
}

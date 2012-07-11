using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Model;
using EyeTracker.Model.Pages.Admin;

namespace EyeTracker.Controllers.Master
{
    public class AdminMasterController : AfterLoginController
    {
        public override Model.Master.AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return Model.Master.AfterLoginMasterModel.MenuItem.Administrator; }
        }
        
        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AdminMasterModel.MenuItem leftMenuSelectedItem, AfterLoginMasterModel.MenuItem selectedItem)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, AdminMasterModel, TViewModel>(GetModel(selectedItem), new AdminMasterModel(leftMenuSelectedItem), viewModel);

            return base.View(model);
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AdminMasterModel.MenuItem leftMenuSelectedItem)
        {
            return View(new AdminMasterModel(leftMenuSelectedItem), viewModel);
        }
    }
}

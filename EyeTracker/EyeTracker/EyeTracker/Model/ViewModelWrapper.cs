using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model
{
    public class ViewModelWrapper<TMasterModel, TViewModel> : MasterViewModelWrapper<TMasterModel>
    {
        public ViewModelWrapper() { }

        public ViewModelWrapper(TMasterModel masterModel, TViewModel viewModel)
            : base(masterModel)
        {
            View = viewModel;
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public TViewModel View { get; set; }
    }
}
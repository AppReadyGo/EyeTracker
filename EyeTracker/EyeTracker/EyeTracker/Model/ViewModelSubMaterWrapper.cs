using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model
{
    public class ViewModelSubMaterWrapper<TMasterModel, TSubMaster, TViewModel>
        : SubMasterViewModelWrapper<TMasterModel, TSubMaster>
    {
        public ViewModelSubMaterWrapper() { }

        public ViewModelSubMaterWrapper(TMasterModel masterModel, TSubMaster subMasterModel, TViewModel viewModel)
            : base(masterModel, subMasterModel)
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
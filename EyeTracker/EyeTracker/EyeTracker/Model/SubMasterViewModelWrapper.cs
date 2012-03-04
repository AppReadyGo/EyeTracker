using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model
{
    public class SubMasterViewModelWrapper<TMasterModel, TSubMaster> : MasterViewModelWrapper<TMasterModel>
    {
        public SubMasterViewModelWrapper() { }

        public SubMasterViewModelWrapper(TMasterModel masterModel, TSubMaster subMasterModel)
            : base(masterModel)
        {
            SubMaster = subMasterModel;
        }

        /// <summary>
        /// Gets or sets the master model.
        /// </summary>
        /// <value>The master model.</value>
        public TSubMaster SubMaster { get; set; }
    }
}
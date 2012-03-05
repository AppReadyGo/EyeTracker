using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model
{
    public class MasterViewModelWrapper<TMasterModel>
    {
        public MasterViewModelWrapper() { }

        public MasterViewModelWrapper(TMasterModel masterModel)
        {
            Master = masterModel;
        }

        /// <summary>
        /// Gets or sets the master model.
        /// </summary>
        /// <value>The master model.</value>
        public TMasterModel Master { get; set; }
    }

    public class MasterViewModelWrapper<TMasterModel, TSubMasterModel> : MasterViewModelWrapper<TMasterModel>
    {
        public MasterViewModelWrapper() { }

        public MasterViewModelWrapper(TMasterModel masterModel, TSubMasterModel subMasterModel)
            : base(masterModel)
        {
            this.SubMaster = subMasterModel;
        }

        public TMasterModel Master { get; set; }
        public TSubMasterModel SubMaster { get; set; }
    }
}
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
}
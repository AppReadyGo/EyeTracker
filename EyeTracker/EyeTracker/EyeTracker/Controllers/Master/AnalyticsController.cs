﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model.Master;
using EyeTracker.Model;

namespace EyeTracker.Controllers.Master
{
    public class AnalyticsController : AfterLoginController
    {//SubMasterViewModelWrapper<AfterLoginViewModel, AnalyticsModel>
        /* 
        protected ActionResult View(object viewModel)
        {
            var model = ViewModelSubMaterWrapper<AfterLoginViewModel, AnalyticsModel>();
        }

        protected virtual void HttpGetAttribute()
        {
            return null;
        }
       
        protected virtual object CreateModel(object model, TMasterViewModel masterModel)
        {
            var modelType = typeof(object);

            if (model != null)
                modelType = model.GetType();

            var types = new[] { typeof(TMasterViewModel), modelType };
            Type generic = typeof(ViewModelWrapper<,>).MakeGenericType(types);

            return Activator.CreateInstance(generic, masterModel, model);
        }*/
    }
}

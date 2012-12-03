using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using EyeTracker.Core.Services;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Windsor;
using EyeTracker.DAL.Domain;
using EyeTracker.Domain.Model.Events;
using EyeTracker.Model.Pages.Analytics;
using EyeTracker.Common;
using System.Drawing;

namespace EyeTracker.CustomModelBinders
{

    /// <summary>
    /// 
    /// </summary>
    public class ABFilterParametersModelBinder : FilterParametersModelBinder
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var mState = bindingContext.ModelState;
            var queryString = controllerContext.HttpContext.Request.QueryString;
            try
            {
                var result = GetFilterModel<ABFilterParametersModel>(mState, queryString);
                string value = queryString["sp"];
                result.SecondPath = string.IsNullOrEmpty(value) ? queryString["p"] : value;
                return result;
            }
            catch (Exception exp)
            {
                Guid guid = log.WriteError(exp, "Error to parse:{0}", queryString);
                mState.AddModelError("GeneralError", "Please contact to customer service: customerservice@mobillify.com, error guid:" + guid.ToString());
                return null;
            }
        }
    }
}
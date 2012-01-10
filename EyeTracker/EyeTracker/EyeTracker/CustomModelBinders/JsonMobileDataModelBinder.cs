using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Web.Mvc;
using EyeTracker.Core.Services;
using EyeTracker.Windsor;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using EyeTracker.Domain.Model.Events;
using EyeTracker.Common.Logger;
using System.Reflection;

namespace EyeTracker.CustomModelBinders
{
    public class JsonMobileDataModelBinder : DefaultModelBinder
    {
        private static readonly ApplicationLogging log = 
            new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// convert data to PackageEvent 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public override object BindModel(ControllerContext controllerContext, 
                                         ModelBindingContext bindingContext)
        {
            ModelStateDictionary mState = bindingContext.ModelState;
            string json = HttpUtility.UrlDecode(controllerContext.HttpContext.Request.Form.ToString());
            try
            {   
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonPackage));
                MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                var package = serializer.ReadObject(ms) as JsonPackage;

                //return ParseVisitEvents(mState, package);
                return EventParser<PackageEvent>.Parse(mState, package);
            }
            catch (Exception exp)
            {
                //TODO : change it , should be another replay 
                Guid guid = log.WriteError(exp, "JsonVisitInfoModelBinder Error, json:{0}", json);
                mState.AddModelError("GeneralError", "Please contact to customer service: " +
                            "customerservice@mobillify.com, error guid:" + guid.ToString());
            }
            return null;
        }



    }
}
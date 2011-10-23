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

namespace EyeTracker.CustomModelBinders
{
    public class JsonVisitInfoModelBinder : DefaultModelBinder
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        
        [DataContract]
        private class JsonVisitInfo
        {
            [DataMember(Name = "cid")]
            public string Key { get; set; }

            [DataMember(Name = "d")]
            public string Date { get; set; }

            [DataMember(Name = "sw")]
            public int ScreenWidth { get; set; }

            [DataMember(Name = "sh")]
            public int ScreenHeight { get; set; }

            [DataMember(Name = "cw")]
            public int ClientWidth { get; set; }

            [DataMember(Name = "ch")]
            public int ClientHeight { get; set; }

            [DataMember(Name = "uri")]
            public string PageUri { get; set; }
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var visitInfo = new VisitEvent();
            ModelStateDictionary mState = bindingContext.ModelState;
            string json = HttpUtility.UrlDecode(controllerContext.HttpContext.Request.Form.ToString());
            try
            {
                IAnalyticsService service = WindsorFactory.Resolve<IAnalyticsService>();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonVisitInfo));
                MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                var visitInfoModel = serializer.ReadObject(ms) as JsonVisitInfo;

                //Get date
                DateTime date;
                if (!DateTime.TryParse(visitInfoModel.Date, out date))
                {
                    mState.Add("Date(d)", new ModelState { });
                    mState.AddModelError("Date(d)", "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                }

                if (mState.IsValid)
                {
                    visitInfo.Key = visitInfoModel.Key;
                    //TODO: Add date
                    //visitInfo.Date = date;
                    visitInfo.ScreenWidth = visitInfoModel.ScreenWidth;
                    visitInfo.ScreenHeight = visitInfoModel.ScreenHeight;
                    visitInfo.ClientWidth = visitInfoModel.ClientWidth;
                    visitInfo.ClientHeight = visitInfoModel.ClientHeight;
                    visitInfo.Path = visitInfoModel.PageUri;
                }
            }
            catch (Exception exp)
            {
                Guid guid = log.WriteError(exp, "JsonVisitInfoModelBinder Error, json:{0}", json);
                mState.AddModelError("GeneralError", "Please contact to customer service: customerservice@mobillify.com, error guid:" + guid.ToString());
            }
            return visitInfo;
        }
    }
}
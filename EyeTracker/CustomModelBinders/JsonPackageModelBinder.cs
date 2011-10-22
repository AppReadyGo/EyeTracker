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
using AutoMapper;
using EyeTracker.Model;
using EyeTracker.DAL.Domain;
using EyeTracker.Domain.Model.Events;

namespace EyeTracker.CustomModelBinders
{
    public class JsonPackageModelBinder : DefaultModelBinder
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        [DataContract]
        public class JsonPackage
        {
            [DataMember(Name = "vid")]
            public long VisitId { get; set; }

            [DataMember(Name = "vpd")]
            public JsonViewPart[] ViewParts { get; set; }

            [DataMember(Name = "cd")]
            public JsonClick[] Clicks { get; set; }
        }

        [DataContract]
        public class JsonViewPart
        {
            [DataMember(Name = "sd")]
            public string StrStartDate { get; set; }

            [DataMember(Name = "sl")]
            public int ScrollLeft { get; set; }

            [DataMember(Name = "st")]
            public int ScrollTop { get; set; }

            [DataMember(Name = "fd")]
            public string StrFinishDate { get; set; }
        }

        [DataContract]
        public class JsonClick
        {
            [DataMember(Name = "d")]
            public string StrDate { get; set; }

            [DataMember(Name = "cx")]
            public int ClientX { get; set; }

            [DataMember(Name = "cy")]
            public int ClientY { get; set; }
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            JsonPackage package = null;
            PackageEvent packageEvent = new PackageEvent() { clicks = new List<ClickEvent>(), parts = new List<ViewPartEvent>() };
            ModelStateDictionary mState = bindingContext.ModelState;
            string json = HttpUtility.UrlDecode(controllerContext.HttpContext.Request.Form.ToString());
            try
            {
                IAnalyticsService service = WindsorFactory.Resolve<IAnalyticsService>();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonPackage));
                MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                package = serializer.ReadObject(ms) as JsonPackage;

                DateTime date;
                foreach (var curClick in package.Clicks)
                {
                    //Get date
                    if (!DateTime.TryParse(curClick.StrDate, out date))
                    {
                        mState.Add("Click.StrDate(d)", new ModelState { });
                        mState.AddModelError("Click.StrDate(d)", "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                    }
                    if (mState.IsValid)
                    {
                        packageEvent.clicks.Add(new ClickEvent()
                        {
                            Date = date,
                            VisitInfoId = package.VisitId,
                            ClientX = curClick.ClientX,
                            ClientY = curClick.ClientY
                        });
                    }
                    else
                    {
                        break;
                    }
                }

                DateTime toDate;
                foreach (var curPart in package.ViewParts)
                {
                    //Get date
                    if (!DateTime.TryParse(curPart.StrStartDate, out date))
                    {
                        mState.Add("ViewPart(vpd).StrStartDate(sd)", new ModelState { });
                        mState.AddModelError("ViewPart(vpd).StrStartDate(sd)", "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                    }
                    if (!DateTime.TryParse(curPart.StrFinishDate, out toDate))
                    {
                        mState.Add("ViewPart(vpd).StrFinishDate(fd)", new ModelState { });
                        mState.AddModelError("ViewPart(vpd).StrFinishDate(fd)", "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                    }
                    if (mState.IsValid)
                    {
                        packageEvent.parts.Add(new ViewPartInfo()
                        {
                            Date = date,
                            VisitInfoId = package.VisitId,
                            TimeSpan = (int)(toDate - date).TotalSeconds,
                            ScrollLeft = curPart.ScrollLeft,
                            ScrollTop = curPart.ScrollTop
                        });
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception exp)
            {
                Guid guid = log.WriteError(exp, "JsonPackageModelBinder Error, json:{0}", json);
                mState.AddModelError("GeneralError", "Please contact to customer service: customerservice@mobillify.com, error guid:" + guid.ToString());
            }
            return packageEvent;
        }
    }
}
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
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        [DataContract]
        public class JsonPackage
        {
            [DataMember(Name = "cid")]
            public string ClientKey { get; set; }

            [DataMember(Name = "sh")]
            public int ScreenHeight { get; set; }

            [DataMember(Name = "sw")]
            public int ScreenWidth { get; set; }

            [DataMember(Name = "sd")]
            public JsonSessionInfo[] SessionInfo { get; set; }
        }

        [DataContract]
        public class JsonSessionInfo
        {
            [DataMember(Name = "uri")]
            public string PageUri { get; set; }

            [DataMember(Name = "cw")]
            public int ClientWidth { get; set; }

            [DataMember(Name = "ch")]
            public int ClientHeight { get; set; }

            [DataMember(Name = "ss")]
            public string SessionStartDate { get; set; }

            [DataMember(Name = "sc")]
            public string SessionCloseDate { get; set; }

            [DataMember(Name = "td")]
            public JsonTouchDetails[] TouchDetails { get; set; }

            [DataMember(Name = "sd")]
            public JsonScrollDetails ScrollDetails { get; set; }
        }

        [DataContract]
        public class JsonTouchDetails
        {
            [DataMember(Name = "d")]
            public string Date { get; set; }

            [DataMember(Name = "cx")]
            public int ClientX { get; set; }

            [DataMember(Name = "cy")]
            public int ClientY { get; set; }

            [DataMember(Name = "p")]
            public int Press { get; set; }
        }

        [DataContract]
        public class JsonScrollDetails
        {
            [DataMember(Name = "std")]
            public JsonTouchDetails StartTouchData { get; set; }

            [DataMember(Name = "ctd")]
            public JsonTouchDetails CloseTouchData { get; set; }
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ModelStateDictionary mState = bindingContext.ModelState;
            string json = HttpUtility.UrlDecode(controllerContext.HttpContext.Request.Form.ToString());
            try
            {
                IAnalyticsService service = WindsorFactory.Resolve<IAnalyticsService>();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonPackage));
                MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                var package = serializer.ReadObject(ms) as JsonPackage;

                return ParseVisitEvents(mState, package);
            }
            catch (Exception exp)
            {
                Guid guid = log.WriteError(exp, "JsonVisitInfoModelBinder Error, json:{0}", json);
                mState.AddModelError("GeneralError", "Please contact to customer service: customerservice@mobillify.com, error guid:" + guid.ToString());
            }
            return null;
        }

        private List<VisitEvent> ParseVisitEvents(ModelStateDictionary mState, JsonPackage package)
        {
            var visitEvents = new List<VisitEvent>();
            foreach (var session in package.SessionInfo)
            {
                DateTime startDate;
                if (!DateTime.TryParse(session.SessionStartDate, out startDate))
                {
                    mState.Add("SessionStartDate(ss)", new ModelState { });
                    mState.AddModelError("SessionStartDate(ss)", "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                }
                DateTime closeDate;
                if (!DateTime.TryParse(session.SessionCloseDate, out closeDate))
                {
                    mState.Add("SessionCloseDate(sc)", new ModelState { });
                    mState.AddModelError("SessionCloseDate(sc)", "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                }
                if (mState.IsValid)
                {
                    var visitEvent = new VisitEvent
                    {
                        Key = package.ClientKey,
                        ScreenHeight = package.ScreenHeight,
                        ScreenWidth = package.ScreenWidth,
                        ClientHeight = session.ClientHeight,
                        ClientWidth = session.ClientWidth,
                        Path = session.PageUri,
                        Date = startDate,
                        //TODO: use close date,
                        Clicks = ParseClicksData(mState, session),
                        ViewParts = ParsePartViewData(mState, session)
                    };
                    visitEvents.Add(visitEvent);
                }
            }
            return visitEvents;
        }

        private IEnumerable<ClickEvent> ParseClicksData(ModelStateDictionary mState, JsonSessionInfo session)
        {
            var clicks = new List<ClickEvent>();
            foreach (var touch in session.TouchDetails)
            {
                DateTime date;
                if (!DateTime.TryParse(session.SessionStartDate, out date))
                {
                    mState.Add("Date(d)", new ModelState { });
                    mState.AddModelError("Date(d)", "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                }
                if (mState.IsValid)
                {
                    var click = new ClickEvent
                    {
                        ClientX = touch.ClientX,
                        ClientY = touch.ClientY,
                        Date = date
                    };
                    clicks.Add(click);
                }
            }
            return clicks;
        }

        private IEnumerable<ViewPartEvent> ParsePartViewData(ModelStateDictionary mState, JsonSessionInfo session)
        {
            var clicks = new List<ViewPartEvent>();
            foreach (var touch in session.TouchDetails)
            {

            }
            return clicks;
        }
    }
}


using EyeTracker.Domain.Model.Events;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
namespace EyeTracker.CustomModelBinders.Parsers
{
    public class JsonPackageParser : IParser
    {
        #region IParser Members

        /// <summary>
        /// Parse JsonPackage input data into model PackageEvent
        /// </summary>
        /// <param name="mState"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        public object ParseToEvent(ModelStateDictionary mState, IPackage package)
        {
            JsonPackage jPackage = (JsonPackage)package;
            PackageEvent packageEvent = new PackageEvent
            {
                Key = jPackage.ClientKey,
                ScreenHeight = jPackage.ScreenHeight,
                ScreenWidth = jPackage.ScreenWidth,
            };

            var sessionEvents = new List<SessionInfoEvent>();

            foreach (var session in jPackage.SessionsInfo)
            {
                DateTime startDate;
                if (!DateTime.TryParse(session.SessionStartDate, out startDate))
                {
                    mState.Add("SessionStartDate(ss)", new ModelState { });
                    mState.AddModelError("SessionStartDate(ss)",
                        "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                }
                DateTime closeDate;
                if (!DateTime.TryParse(session.SessionCloseDate, out closeDate))
                {
                    mState.Add("SessionCloseDate(sc)", new ModelState { });
                    mState.AddModelError("SessionCloseDate(sc)",
                        "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                }

                //TODO : check if we need to set an error ...
                if (mState.IsValid)
                {
 
                }

                var sessionEvent = new SessionInfoEvent
                {
                    CloseDate = closeDate,
                    StartDate = startDate,
                    ClientHeight = session.ClientHeight,
                    ClientWidth = session.ClientWidth,
                    Path = session.PageUri,
                    Clicks          = ParseData<JsonTouchDetails, ClickEvent>
                                        (mState, session.TouchDetails),
                    ScreenViewParts = ParseData<JsonViewAreaDetails, ViewPartEvent> 
                                        (mState, session.ViewAreaDetails),
                    Scrolls         = ParseData<JsonScrollDetails, ScrollEvent>
                                        (mState, session.ScrollDetails)
                };       
                packageEvent.Sessions.Add(sessionEvent); 
            }

            return packageEvent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mSate"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private List<E> ParseData<P, E>(ModelStateDictionary mState, P[] details) where P : IPackage
                                                                                  where E : IEvent
        {
            var info = new List<E>();

            foreach (var detail in details)
            {
                var data = EventParser<E>.Parse(mState, detail);
                if (data != null)
                {
                    info.Add(data);
                }
            }

            return info; ;
        }

        #endregion

    }
}




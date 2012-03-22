

using EyeTracker.Domain.Model.Events;
using System;
using System.Collections.Generic;
using EyeTracker.API.BL.Contract;
using EyeTracker.API.BL;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// MEdiator design pattern 
    /// </summary>
    public class JsonPackageParser : IParser
    {
        #region IParser Members

        /// <summary>
        /// Parse JsonPackage input data into model PackageEvent
        /// </summary>
        /// <param name="mState"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        public object ParseToEvent(IPackage package)
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
                    //mState.Add("SessionStartDate(ss)", new ModelState { });
                    //mState.AddModelError("SessionStartDate(ss)",
                    //    "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                }
                DateTime closeDate;
                if (!DateTime.TryParse(session.SessionCloseDate, out closeDate))
                {
                    //mState.Add("SessionCloseDate(sc)", new ModelState { });
                    //mState.AddModelError("SessionCloseDate(sc)",
                    //    "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                }

                //TODO : check if we need to set an error ...
                if (/*mState.IsValid)*/ true)
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
                                            (session.TouchDetails),
                    ScreenViewParts = ParseData<JsonViewAreaDetails, ViewPartEvent> 
                                            (session.ViewAreaDetails),
                    Scrolls         = ParseData<JsonScrollDetails, ScrollEvent> 
                                            (session.ScrollDetails)
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
        private List<E> ParseData<P,E>(P[] details) where P : IPackage  where E : class
        {
            if (details == null || details.Length == 0)
            {
                return null;
            }
            var info = new List<E>();

            foreach (var item in details)
            {
                var data = (E)EventParser.Parse(item);

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




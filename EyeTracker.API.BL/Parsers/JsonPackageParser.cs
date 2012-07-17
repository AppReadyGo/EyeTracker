

using EyeTracker.Domain.Model.Events;
using System;
using System.Collections.Generic;
using EyeTracker.API.BL.Contract;
using EyeTracker.API.BL;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Common.Interfaces;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// MEdiator design pattern 
    /// </summary>
    public class JsonPackageParser : IParser
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        #region IParser Members

        /// <summary>
        /// Parse JsonPackage input data into model PackageEvent
        /// </summary>
        /// <param name="mState"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        public object ParseToEvent(IPackage package, IEvent parentEvent)
        {
            JsonPackage jPackage = package as JsonPackage;
            if (jPackage == null)
            {
                log.WriteError("JsonPackageParser::ParseToEvent got wrong argument type for 'package'");
                return null;
            }
            if (jPackage.SessionsInfo == null || jPackage.SessionsInfo.Length == 0)
            {
                log.WriteWarning("SessionInfo collection of JsonPackage is empty");
            }
            PackageEvent packageEvent = new PackageEvent
            {
                Key = jPackage.ClientKey,
                ScreenHeight = jPackage.ScreenHeight,
                ScreenWidth = jPackage.ScreenWidth,
                SystemInfo = jPackage.SystemInfo
            };

            var sessionEvents = new List<SessionInfoEvent>();

            foreach (var session in jPackage.SessionsInfo)
            {
                try
                {
                    log.WriteInformation("Try to parse date: {0}", session.SessionStartDate);
                    DateTime startDate;
                    if (!DateTime.TryParse(session.SessionStartDate, out startDate))
                    {
                        //mState.Add("SessionStartDate(ss)", new ModelState { });
                        //mState.AddModelError("SessionStartDate(ss)",
                        //    "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                        log.WriteError("Error parsing session startdate {0}", session.SessionStartDate);
                        continue;
                    }
                    DateTime closeDate;
                    if (!DateTime.TryParse(session.SessionCloseDate, out closeDate))
                    {
                        //mState.Add("SessionCloseDate(sc)", new ModelState { });
                        //mState.AddModelError("SessionCloseDate(sc)",
                        //    "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
                        log.WriteError("Error parsing session closedate {0}", session.SessionCloseDate);
                        continue;
                    }

                    var sessionEvent = new SessionInfoEvent
                    {
                        PackageEvent = packageEvent,
                        CloseDate = closeDate,
                        StartDate = startDate,
                        ClientHeight = session.ClientHeight,
                        ClientWidth = session.ClientWidth,
                        Path = session.PageUri
                    };

                    //if not null should be parsed otherwise new () 
                    sessionEvent.Clicks = session.TouchDetails != null ? 
                        ParseData<JsonTouchDetails, ClickEvent, SessionInfoEvent>(session.TouchDetails, sessionEvent) : new List<ClickEvent>() ;
                    sessionEvent.ScreenViewParts = session.ViewAreaDetails != null ? 
                        ParseData<JsonViewAreaDetails, ViewPartEvent, SessionInfoEvent>(session.ViewAreaDetails, sessionEvent) : new List<ViewPartEvent>();
                    sessionEvent.Scrolls = session.ScrollDetails != null ?
                        ParseData<JsonScrollDetails, ScrollEvent, SessionInfoEvent>(session.ScrollDetails, sessionEvent) : new List<ScrollEvent>();

                    packageEvent.Sessions.Add(sessionEvent);
                }
                catch (Exception ex)
                {
                    log.WriteError(ex, string.Format("Error processing session for page uri {0}", session.PageUri));
                    continue;
                }
            }

            return packageEvent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mSate"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private List<E> ParseData<P,E,K>(P[] details, K parentEvent) where P : IPackage  where E : class where K:IEvent
        {
            if (details == null || details.Length == 0)
            {
                log.WriteWarning("collection of type {0} is empty", typeof(P).Name);
                return null;
            }
            var info = new List<E>();

            foreach (var item in details)
            {
                var data = (E)EventParser.Parse(item, parentEvent);

                if (data != null)
                {
                    info.Add(data);
                }
            }

            return info;
        }

        #endregion

    }
}




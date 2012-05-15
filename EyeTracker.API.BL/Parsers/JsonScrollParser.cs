using EyeTracker.API.BL.Contract;
using EyeTracker.Domain.Model.Events;
using System;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Common.Interfaces;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// MEdiator design pattern 
    /// Concrete Collegue
    /// </summary>
    public class JsonScrollParser : IParser
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        #region IParser Members

        public object ParseToEvent(IPackage package, IEvent parentEvent)
        {
            JsonScrollDetails jScroll = package as JsonScrollDetails;
            if (jScroll == null)
            {
                log.WriteError("JsonScrollParser::ParseToEvent got wrong argument type for 'package'");
                return null;
            }
            try
            {
                ScrollEvent objScrollEvent = new ScrollEvent();
                objScrollEvent.SessionInfoEvent = parentEvent as SessionInfoEvent;
                objScrollEvent.FirstTouch = EventParser.Parse(jScroll.StartTouchData, objScrollEvent) as ClickEvent;
                objScrollEvent.LastTouch = EventParser.Parse(jScroll.CloseTouchData, objScrollEvent) as ClickEvent;
                
                return objScrollEvent;
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error parsing JsonScrollDetails to ScrollEvent");
                return null;
            }
        }

        #endregion
    }
}
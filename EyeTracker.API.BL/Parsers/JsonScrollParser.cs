using EyeTracker.API.BL.Contract;
using EyeTracker.Domain.Model.Events;
using System;
using EyeTracker.Common.Logger;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// MEdiator design pattern 
    /// Concrete Collegue
    /// </summary>
    public class JsonScrollParser : IParser
    {
        private static readonly ApplicationLogging m_objLog = new ApplicationLogging(typeof(JsonScrollParser));
        #region IParser Members

        public object ParseToEvent(IPackage package)
        {
            JsonScrollDetails jScroll = package as JsonScrollDetails;
            if (jScroll == null)
            {
                //todo: log?
                return null;
            }
            try
            {
                return new ScrollEvent()
            {
                FirstTouch  = EventParser.Parse(jScroll.StartTouchData) as ClickEvent,
                LastTouch = EventParser.Parse(jScroll.CloseTouchData) as ClickEvent             
            };
            }
            catch (Exception ex)
            {
                m_objLog.WriteError(ex, "Error parsing JsonScrollDetails to ScrollEvent");
                return null;
            }
        }

        #endregion
    }
}
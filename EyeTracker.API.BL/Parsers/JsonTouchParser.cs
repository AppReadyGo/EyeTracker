using System.Collections.Generic;
using EyeTracker.Domain.Model.Events;
using System;
using System.Linq;
using EyeTracker.API.BL.Contract;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Common.Interfaces;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// Mediator design pattern 
    /// Concrete Collegue
    /// </summary>
    public class JsonTouchParser : IParser
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        #region IParser Members

        public object ParseToEvent(IPackage package, IEvent parentEvent)
        {

            JsonTouchDetails jTouch = package as JsonTouchDetails;
            if (jTouch == null)
            {
                log.WriteError("JsonTouchParser::ParseToEvent got wrong argument type for 'package'");
                return null;
            }

            DateTime date;
            if (!DateTime.TryParse(jTouch.Date, out date))
            {
                log.WriteError("Error parsing JsonTouchParser date {0}", jTouch.Date);
                return null;
            }

            try
            {
                ClickEvent click = parentEvent is ScrollEvent
                    ? (parentEvent as ScrollEvent).SessionInfoEvent.Clicks.FirstOrDefault(c => c.Date == date)
                    : null;

                //TODO : Yura : ask if wee nedd to check the state
                if (click == null)
                {
                    click = new ClickEvent
                    {
                        ClientX = jTouch.ClientX,
                        ClientY = jTouch.ClientY,
                        Date = date,
                        Orientation = jTouch.Orientation,
                        SessionInfoEvent = parentEvent as SessionInfoEvent
                    };
                }
                else
                {
                    click.ScrollEvent = parentEvent as ScrollEvent;
                }
                return click;
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error parsing JsonTouchDetails to ClickEvent");
                return null;
            }
        }

        #endregion
    }
}
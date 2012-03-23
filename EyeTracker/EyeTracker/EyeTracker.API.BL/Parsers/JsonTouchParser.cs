using System.Collections.Generic;
using EyeTracker.Domain.Model.Events;
using System;
using System.Linq;
using EyeTracker.API.BL.Contract;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// MEdiator design pattern 
    /// Concrete Collegue
    /// </summary>
    public class JsonTouchParser : IParser
    {
        #region IParser Members

        public object ParseToEvent(IPackage package, IEvent parentEvent)
        {

            JsonTouchDetails jTouch = (JsonTouchDetails)package;
            DateTime date;
            if (!DateTime.TryParse(jTouch.Date, out date))
            {
                //mState.Add("Date(d)", new ModelState { });
                //mState.AddModelError("Date(d)", "Wrong format must be: DDD, dd MMM yyyy HH:mm:ss GMT");
            }

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
                    SessionInfoEvent = parentEvent as SessionInfoEvent
                };
            }
            else
            {
                click.ScrollEvent = parentEvent as ScrollEvent;
            }
            return click;
             
        }

        #endregion
    }
}
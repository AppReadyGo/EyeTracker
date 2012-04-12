using EyeTracker.API.BL.Contract;
using EyeTracker.Domain.Model.Events;
using System;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// MEdiator design pattern 
    /// Concrete Collegue
    /// </summary>
    public class JsonViewAreaParser : IParser
    {
        #region IParser Members

        public object ParseToEvent(IPackage package, IEvent parentEvent)
        {
            JsonViewAreaDetails jVAD = package as JsonViewAreaDetails;
            if (jVAD == null)
            {
                return null;
            }

            ViewPartEvent objViewPart = new ViewPartEvent()
            {
                SessionInfoEvent = parentEvent as SessionInfoEvent,
                StartDate = DateTime.Parse(jVAD.StartDate),
                FinishDate = DateTime.Parse(jVAD.FinishDate),
                ScrollLeft = jVAD.CoordX,
                ScrollTop = jVAD.CoordY,
                Orientation = jVAD.Orientation,
                VisitInfoId = 0   //ask??
            };

            return objViewPart;
        }

        #endregion
    }
}
using EyeTracker.API.BL.Contract;
using EyeTracker.Domain.Model.Events;
using System;
using EyeTracker.Common.Logger;
using System.Reflection;
namespace EyeTracker.API.BL.Parsers
{

    /// <summary>
    /// MEdiator design pattern 
    /// Concrete Collegue
    /// </summary>
    public class JsonViewAreaParser : IParser
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        #region IParser Members

        public object ParseToEvent(IPackage package, IEvent parentEvent)
        {
            JsonViewAreaDetails jVAD = package as JsonViewAreaDetails;
            if (jVAD == null)
            {
                log.WriteError("JsonViewAreaParser::ParseToEvent got wrong argument type for 'package'");
                return null;
            }
            DateTime startDate;
            if (!DateTime.TryParse(jVAD.StartDate, out startDate))
            {
                log.WriteError("Error parsing JsonViewAreaDetails startdate {0}", jVAD.StartDate);
                return null;
            }
            DateTime finishDate;
            if (!DateTime.TryParse(jVAD.FinishDate, out finishDate))
            {
                log.WriteError("Error parsing JsonViewAreaDetails finishdate {0}", jVAD.FinishDate);
                return null;
            }

            ViewPartEvent objViewPart = new ViewPartEvent()
            {
                SessionInfoEvent = parentEvent as SessionInfoEvent,
                StartDate = startDate,
                FinishDate = finishDate,
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
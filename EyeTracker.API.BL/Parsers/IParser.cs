using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.API.BL.Contract;
using EyeTracker.Domain.Model.Events;
//using EyeTracker.Domain.Model.Events;


namespace EyeTracker.API.BL.Parsers
{
    public interface IParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mState"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        object ParseToEvent(IPackage package, IEvent parentEvent);
    }
}

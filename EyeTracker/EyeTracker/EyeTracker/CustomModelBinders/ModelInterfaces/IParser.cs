using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.Events;
using System.Web.Mvc;

namespace EyeTracker.CustomModelBinders.Parsers
{
    public interface IParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mState"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        object ParseToEvent(ModelStateDictionary mState, IPackage package);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Domain.Model.Events;
using EyeTracker.CustomModelBinders.Parsers;

namespace EyeTracker.CustomModelBinders
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="E">Event</typeparam>
    internal  class EventParser<E> where E : IEvent
    {

        /// <summary>
        /// TODO : move this dictionary to configuration 
        /// </summary>
        private static Dictionary<Type, Func<ModelStateDictionary, IPackage, object>> m_parser =
            new Dictionary<Type, Func<ModelStateDictionary, IPackage, object>>();

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        internal static E Parse(ModelStateDictionary state, IPackage package)
        {
            return (E) m_parser[package.GetType()].Invoke(state, package);
        }

        /// <summary>
        /// remove this configuration fo MEF/Windsorn
        /// </summary>
        static EventParser()
        {
            m_parser.Add(typeof(JsonPackage), new JsonPackageParser().ParseToEvent);
            m_parser.Add(typeof(JsonScrollDetails), new JsonScrollParser().ParseToEvent);
            m_parser.Add(typeof(JsonViewAreaDetails), new JsonViewAreaParser().ParseToEvent);
            m_parser.Add(typeof(JsonTouchDetails), new JsonTouchParser().ParseToEvent);
        }

    }
}

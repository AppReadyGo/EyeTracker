using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.API.BL.Contract;
using EyeTracker.API.BL.Parsers;

namespace EyeTracker.API.BL
{

    /// <summary>
    /// MEdiator design pattern 
    /// ConcreteMediator
    /// </summary>
    /// <typeparam name="E">Event</typeparam>
    public  class EventParser
    {

        /// <summary>
        /// TODO : move this dictionary to configuration 
        /// </summary>
        private static Dictionary<Type, Func<IPackage, object>> m_parser =
            new Dictionary<Type, Func<IPackage, object>>();

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static object Parse(IPackage package)
        {
            return m_parser[package.GetType()].Invoke(package);
        }

      
        
        static EventParser()
        {
            m_parser.Add(typeof(JsonPackage), new JsonPackageParser().ParseToEvent);
            m_parser.Add(typeof(JsonScrollDetails), new JsonScrollParser().ParseToEvent);
            m_parser.Add(typeof(JsonViewAreaDetails), new JsonViewAreaParser().ParseToEvent);
            m_parser.Add(typeof(JsonTouchDetails), new JsonTouchParser().ParseToEvent);
        }

    }
}

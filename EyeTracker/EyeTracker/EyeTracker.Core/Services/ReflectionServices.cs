using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Reflection;
using EyeTracker.Common.Logger;

namespace EyeTracker.Core.Services
{
    public sealed class ReflectionServices
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        #region Singleton implementation
        private static readonly ReflectionServices m_objInstance = new ReflectionServices();

        public static ReflectionServices MyInstance
        {
            get
            {
                return m_objInstance;
            }
        }

        private ReflectionServices()
        {
            MyAssemblies = new Dictionary<string, Assembly>();
            var objCurrAsm = Assembly.GetExecutingAssembly();
            MyAssemblies.Add(objCurrAsm.GetName().Name, objCurrAsm);
        }

        #endregion 

        private Dictionary<string, Assembly> MyAssemblies { get; set; }

        public Type GetType(string p_strAssemblyFullName, string p_strTypeFullName)
        {
            Assembly objAsm = null;
            Type objType = null;
            try
            {
                if (MyAssemblies.ContainsKey(p_strAssemblyFullName))
                {
                    objAsm = MyAssemblies[p_strAssemblyFullName];
                }
                else
                {
                    objAsm = Assembly.Load(p_strAssemblyFullName);
                    MyAssemblies.Add(p_strAssemblyFullName, objAsm);
                }
                objType = objAsm.GetType(p_strTypeFullName, false);
            }
            catch (Exception ex)
            {
                objType = null;
                log.WriteError(ex, string.Format("Error creating type {0} from assembly {1}", p_strTypeFullName, p_strAssemblyFullName));
            }
            return objType;
        }

        public dynamic CreateInstance(Type p_objType)
        {
            //Contract.Requires<NullReferenceException>(p_objType != null);
            dynamic objInstance;
            try
            {
                //Contract.Requires<NullReferenceException>(p_objType != null);
                objInstance = Activator.CreateInstance(p_objType);
            }
            catch (Exception ex)
            {
                objInstance = null;
                log.WriteError(ex, string.Format("Error creating instance of type {0}", p_objType.FullName));
            }
            return objInstance;
        }
    }
}

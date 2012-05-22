using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common;
using EyeTracker.Domain.Model.Events;
using EyeTracker.Domain.Repositories;
using System.Diagnostics.Contracts;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Common.Interfaces;

namespace EyeTracker.Core.Services
{
    /// <summary>
    /// expose Data Repository services 
    ///
    /// </summary>
    //public interface IDataRepositoryServices
    //{
    //    /// <summary>
    //    /// Handle package event will procced operations with PackageEvent
    //    /// </summary>
    //    /// <param name="packageEvent"></param>
    //    /// <returns>OK - if succeeded</returns>
    //    OperationResult HandlePackageEvent(PackageEvent packageEvent);
    //}

    public class DataRepositoryServices : IStoreServices
        //where T : class, IDataRepository, new()
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        private IDataRepository m_objDataRepository = null;

        public DataRepositoryServices(string p_strAssemblyFullName, string p_strTypeFullName)
        {
            Type objType = ReflectionServices.MyInstance.GetType(p_strAssemblyFullName, p_strTypeFullName);
            IDataRepository objRepositoryInstance = ReflectionServices.MyInstance.CreateInstance(objType);
            Init(objRepositoryInstance);
        }

        public DataRepositoryServices(IDataRepository dataRepository)
        {
            //Contract.Requires<NullReferenceException>(dataRepository != null);
            Init(dataRepository);
        }

        [Obsolete("Use one of the overloaded ctors")]
        public DataRepositoryServices() : this(new DataRepository())
        {

        }

        private void Init(IDataRepository p_objDataRepository)
        {
            m_objDataRepository = p_objDataRepository;
        }

        public OperationResult HandlePackageEvent(IPackageEvent packageEvent)
        {
            //Contract.Requires<NullReferenceException>(packageEvent is PackageEvent);
            OperationResult objResult = null;

            try
            {
                m_objDataRepository.AddPackageEvent(packageEvent);
                objResult = new OperationResult();
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error saving package event");
                objResult = new OperationResult(ex);
            }
            
            return objResult;
        }
    }
}

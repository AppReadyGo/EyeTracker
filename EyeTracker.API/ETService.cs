using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using EyeTracker.API.BL.Contract;
using EyeTracker.API.BL;
using EyeTracker.Core.Services;
using EyeTracker.Domain.Model.Events;
using EyeTracker.Common;
using System.Threading;
using EyeTracker.Common.Logger;
using System.Reflection;
using System.ComponentModel;
using EyeTracker.Domain.Repositories;

namespace EyeTracker.API
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class ETService
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

          /// <summary>
          /// Check service status 
          /// </summary>
          /// <returns></returns>
        [WebGet(UriTemplate = "status")]
        [Description("Test method")]
        public bool GetStatus()
        {
            log.WriteInformation("WriteVerbose: Call to GetStatus");
            return true;
        }


        /// <summary>
        /// Submit Json Package
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "submit", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        [Description("Receives the package and stores it in DB")]
        public bool SubmitPackage(string instance)
        {
            log.WriteInformation("WriteVerbose: SubmitStatus");

            try
            {
                if (String.IsNullOrWhiteSpace(instance))
                {
                    log.WriteWarning("SubmitPackage got empty string");
                    return false;
                }
 
                JsonPackage package = Deserialize<JsonPackage>(instance);
                if (package == null)
                {
                    //Console.WriteLine("PROBLEMMMMMMMMM");
                    log.WriteError("SubmitPackage got smth that can't be deserialized");
                    //ApplicationLogging.WriteError(this.GetType(), "SubmitPackage : problem with JsonPackage");
                    return false;
                }

                PackageEvent objParserResult = EventParser.Parse(package) as PackageEvent;
                EventsServices objEventSvc = new EventsServices("EyeTracker.Domain", "EyeTracker.Domain.Repositories.EventsRepository");
                OperationResult objSaveResult = objEventSvc.HandlePackageEvent(objParserResult);
                log.WriteVerbose("return result is " + !objSaveResult.HasError);

                #region TEMP
                DataRepositoryServices objDataRepositorySvc = new DataRepositoryServices("EyeTracker.Domain", "EyeTracker.Domain.Repositories.DataRepository");
                objDataRepositorySvc.HandlePackageEvent(objParserResult);
                #endregion

                return !objSaveResult.HasError;
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error in SubmitPackage");
                return false;
            }
        }


        /// <summary>
        /// Submit Json Package
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "data", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        [Description("Receives the package and stores it in DB")]
        public bool SubmitData(FPData instanceVal)
        {

            log.WriteInformation("WriteVerbose: SubmitData");

            try
            {

                string instance = instanceVal.InstanceValue;

                JsonPackage package = Deserialize<JsonPackage>(instance);
                if (package == null)
                {
                    //Console.WriteLine("PROBLEMMMMMMMMM");
                    log.WriteError("SubmitPackage got smth that can't be deserialized");
                    //ApplicationLogging.WriteError(this.GetType(), "SubmitPackage : problem with JsonPackage");
                    return false;
                }

                PackageEvent objParserResult = EventParser.Parse(package) as PackageEvent;
                EventsServices objEventSvc = new EventsServices("EyeTracker.Domain", "EyeTracker.Domain.Repositories.EventsRepository");
                OperationResult objSaveResult = objEventSvc.HandlePackageEvent(objParserResult);
                log.WriteVerbose("return result is " + !objSaveResult.HasError);

                #region TEMP
                DataRepositoryServices objDataRepositorySvc = new DataRepositoryServices("EyeTracker.Domain", "EyeTracker.Domain.Repositories.DataRepository");
                objDataRepositorySvc.HandlePackageEvent(objParserResult);
                #endregion

                return !objSaveResult.HasError;
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error in SubmitPackage");
                return false;
            }
        }



        /// <summary>
        /// JSON object deserialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_strObject"></param>
        /// <returns></returns>
        private static T Deserialize<T>(string p_strObject) where T: class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            byte[] bytes = Encoding.UTF8.GetBytes(p_strObject);
            MemoryStream ms = new MemoryStream(bytes);
            object obj = serializer.ReadObject(ms);

            return obj as T;
        }

  
    }
}

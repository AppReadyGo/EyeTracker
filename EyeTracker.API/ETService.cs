using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using EyeTracker.API.BL;
using EyeTracker.API.BL.Contract;
using EyeTracker.Common.Logger;
using EyeTracker.Core.Services;
using EyeTracker.Domain.Model.Events;

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
        public bool SubmitPackage(FPData instance)
        {
            log.WriteInformation("Info: Status arrived");

            try
            {
                if (null == instance || String.IsNullOrWhiteSpace(instance.val))
                {
                    log.WriteWarning("SubmitPackage is null or the val is empty");
                    return false;
                }
 
                JsonPackage package = Deserialize<JsonPackage>(instance.val);
                if (package == null)
                {
                    //Console.WriteLine("PROBLEMMMMMMMMM");
                    log.WriteError("SubmitPackage got smth that can't be deserialized");
                    //ApplicationLogging.WriteError(this.GetType(), "SubmitPackage : problem with JsonPackage");
                    return false;
                }

                PackageEvent objParserResult = EventParser.Parse(package) as PackageEvent;
                //objParserResult.Ip = instance.Ip;
                objParserResult.Ip = "Not needed";

                //---EventsServices objEventSvc = new EventsServices("EyeTracker.Domain", "EyeTracker.Domain.Repositories.EventsRepository");
                //---OperationResult objSaveResult = objEventSvc.HandlePackageEvent(objParserResult);
                //---log.WriteVerbose("return result is " + !objSaveResult.HasError);

                

                //#region TEMP
                DataRepositoryServices objDataRepositorySvc = new DataRepositoryServices("EyeTracker.Domain", "EyeTracker.Domain.Repositories.DataRepository");
                return !objDataRepositorySvc.HandlePackageEvent(objParserResult).HasError;
                //#endregion


                
                //---return !objSaveResult.HasError;
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error in SubmitPackage");
                return false;
            }
        }


        //DELETED by Pavel
        /// <summary>
        /// It's worthwhile to know that behind the scenes WCF is passing the client's IP and port (along some other information) with each message as the properties for the message. 
        /// This is happening for services hosted on HTTP or TCP protocols so the important point is here and you can't apply this code for other protocols.
        /// </summary>
        /// <returns></returns>
        //private RequestInfo GetRequestInfo()
        //{
            
        //    RequestInfo objRI = new RequestInfo();
        //    try
        //    {
        //        OperationContext context = OperationContext.Current;
        //        MessageProperties messageProperties = context.IncomingMessageProperties;
        //        RemoteEndpointMessageProperty endpointProperty =
        //            messageProperties[RemoteEndpointMessageProperty.Name]
        //            as RemoteEndpointMessageProperty;
        //        if (endpointProperty != null)
        //        {
        //            objRI.Ip = endpointProperty.Address;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.WriteError(ex, "Error retrieving client Ip");
        //    }
        //    return objRI;
        //}
        

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

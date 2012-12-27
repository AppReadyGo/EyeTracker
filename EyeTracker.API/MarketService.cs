using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using EyeTracker.Common.Logger;
using System.Reflection;

namespace EyeTracker.API
{

    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class MarketService
    {

        private static readonly ApplicationLogging m_log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        [WebGet(UriTemplate = "app/{id}")]
        public Stream DownloadApp(string id)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/txt";
            FileStream f = new FileStream("C:\\Test.txt", FileMode.Open);
            int length = (int)f.Length;
            WebOperationContext.Current.OutgoingResponse.ContentLength = length;
            byte[] buffer = new byte[length];
            int sum = 0;
            int count;
            while ((count = f.Read(buffer, sum, length - sum)) > 0)
            {
                sum += count;
            }
            f.Close();
            return new MemoryStream(buffer);
        }

        [WebGet(UriTemplate = "catalogue/{uId}")]
        public string GetCalatogue(string uId)
        {
            return "hello";
        }

        [WebInvoke(UriTemplate = "reg/{req}")]
        public string Register(string req)
        {
            return null;
        }

        [WebInvoke(UriTemplate = "login/{req}")]
        public string LogIn(string req)
        {
            return null;
        }

        [WebInvoke(UriTemplate = "confirm/{ack}")]
        public void DownloadConfirmation(string confirm)
        { 
            
        }
    }
}
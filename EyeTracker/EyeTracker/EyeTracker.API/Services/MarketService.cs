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
using EyeTracker.API.Data;
using System.EnterpriseServices;

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

        [WebInvoke(UriTemplate = "dapp", Method="POST", RequestFormat = WebMessageFormat.Json)]
        [Description("Download app from the server")]
        public Stream DownloadApp(MarketData data)
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

        [WebInvoke(UriTemplate = "catalogue", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        [Description("Retrieve apps list")]
        public string GetCalatogue(MarketData data)
        {
            return "hello";
        }

        [WebInvoke(UriTemplate = "reg")]
        [Description("User registration")]
        public string Register(MarketData data)
        {
            return null;
        }

        [WebInvoke(UriTemplate = "login", Method="POST", RequestFormat=WebMessageFormat.Json)]
        [Description("UserLogin")]
        public string LogIn(MarketData data)
        {
            return null;
        }

        [WebInvoke(UriTemplate = "confirm", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        [Description("Download app confirmation")]
        public void DownloadConfirm(MarketData data)
        { 
            
        }
    }
}
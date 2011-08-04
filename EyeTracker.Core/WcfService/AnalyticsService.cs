using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EyeTracker.Common;
using System.ServiceModel.Activation;

namespace EyeTracker.Core.Wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAnalyticsService
    {

        [OperationContract]
        //[WebGet(UriTemplate = "/visit/{jsonData}")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        OperationResult<long> Visit(string jsonData);

        [OperationContract]
        //[WebGet(UriTemplate = "/package")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        OperationResult Package(string json);

        // TODO: Add your service operations here
    }


    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AnalyticsService : IAnalyticsService
    {
        public OperationResult<long> Visit(string jsonData)
        {
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(VisitInfoViewModel));

            //MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonData));
            //VisitInfoViewModel visitInfo = serializer.ReadObject(ms) as VisitInfoViewModel;
            ////TODO: Add the visit to db
            //OperationResult<long> res = null;

            return new OperationResult<long>();
        }

        public OperationResult Package(string json)
        {
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AnalyticsPackage));

            //MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(Request["d"]));
            //AnalyticsPackage packageObject = serializer.ReadObject(ms) as AnalyticsPackage;
            //TODO: Add the package to db
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
            return new OperationResult(ErrorNumber.General);
        }
    }
}

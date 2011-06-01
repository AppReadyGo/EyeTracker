using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using EyeTracker.Common;

namespace EyeTracker.Wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class AnalyticsService : IAnalyticsService
    {
        public OperationResult<long> Visit(string jsonData)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(VisitInfoViewModel));

            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonData));
            VisitInfoViewModel visitInfo = serializer.ReadObject(ms) as VisitInfoViewModel;
            //TODO: Add the visit to db
            OperationResult<long> res = null;

            return res;
        }

        public OperationResult Package(string jsonData)
        {
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AnalyticsPackage));

            //MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(Request["d"]));
            //AnalyticsPackage packageObject = serializer.ReadObject(ms) as AnalyticsPackage;
            //TODO: Add the package to db
            OperationResult res = null;

            return res;
        }
    }
}

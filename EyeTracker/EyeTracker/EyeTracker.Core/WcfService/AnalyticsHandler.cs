using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using EyeTracker.Common;
using System.Runtime.Serialization.Json;
using System.IO;
using EyeTracker.DAL.Domain;
using EyeTracker.Domain.Model.Events;

namespace EyeTracker.Core.WcfService
{
    public class AnalyticsHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public OperationResult<long> Visit(string jsonData)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(VisitEvent));

            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonData));
            VisitEvent visitEvent = serializer.ReadObject(ms) as VisitEvent;
            ////TODO: Add the visit to db
            //OperationResult<long> res = null;

            return new OperationResult<long>();
        }

        public OperationResult Package(string jsonData)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PackageEvent));

            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonData));
            PackageEvent packageObject = serializer.ReadObject(ms) as PackageEvent;
            //TODO: Add the package to db
            return new OperationResult(ErrorNumber.General);
        }
    }
}

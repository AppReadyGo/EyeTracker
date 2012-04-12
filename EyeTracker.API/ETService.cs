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
        // TODO: Implement the collection resource that will contain the SampleItem instances

        [WebGet(UriTemplate = "")]
        public bool Post()
        {  
            //return ParseVisitEvents(mState, package);
            //return (bool)EventParser.Parse(package); 
            JsonPackage objPackage = GetPackage();
            PackageEvent objParserResult = EventParser.Parse(objPackage) as PackageEvent;
            EventsServices objEventSvc = new EventsServices();
            OperationResult objSaveResult = objEventSvc.HandlePackageEvent(objParserResult);
            return !objSaveResult.HasError;
        }

        [WebInvoke(UriTemplate = "Update/{id}/{instance}", Method = "PUT")]
        public bool Update(string id, IPackage instance)
        {
            // TODO: Update the given instance of SampleItem in the collection
            throw new NotImplementedException();
        }

        private JsonPackage GetPackage()
        {
            JsonPackage objJP = new JsonPackage();
            objJP.ClientKey = "123";
            objJP.ScreenHeight = 1280;
            objJP.ScreenWidth = 600;
            objJP.SessionsInfo = new JsonSessionInfo[1];
            objJP.SessionsInfo[0] = new JsonSessionInfo();
            objJP.SessionsInfo[0].ClientHeight = 1000;
            objJP.SessionsInfo[0].ClientWidth = 500;
            objJP.SessionsInfo[0].PageUri = "myPageUri";
            objJP.SessionsInfo[0].SessionStartDate = DateTime.Now.AddSeconds(-30).ToString();
            objJP.SessionsInfo[0].SessionCloseDate = DateTime.Now.ToString();
            objJP.SessionsInfo[0].TouchDetails = GetTouchDetails();
            objJP.SessionsInfo[0].ScrollDetails = GetScrollDetails(objJP.SessionsInfo[0].TouchDetails[0], objJP.SessionsInfo[0].TouchDetails[2]);
            objJP.SessionsInfo[0].ViewAreaDetails = GetViewAreaDetails();

            return objJP;
        }

        private JsonViewAreaDetails[] GetViewAreaDetails()
        {
            var colViewAreaDetails = new JsonViewAreaDetails[]
            {
                GetViewAreaDetail(),
                GetViewAreaDetail(),
                GetViewAreaDetail()
            };
            return colViewAreaDetails;

        }

        private JsonViewAreaDetails GetViewAreaDetail()
        {
            Random r = new Random();
            var objViewAreaDetail = new JsonViewAreaDetails();
            objViewAreaDetail.CoordX = r.Next(0, 600);
            objViewAreaDetail.CoordY = r.Next(0, 1280);
            objViewAreaDetail.StartDate = DateTime.Now.AddSeconds(-10).ToString();
            objViewAreaDetail.FinishDate = DateTime.Now.AddSeconds(-9).ToString();
            objViewAreaDetail.Orientation = 7;

            Thread.Sleep(1000);

            return objViewAreaDetail;
        }

        private JsonTouchDetails[] GetTouchDetails()
        {
            var colTouchDeatils = new JsonTouchDetails[]
            {
                GetTouchDetail(),
                GetTouchDetail(),
                GetTouchDetail()
            };
            return colTouchDeatils;
        }

        private JsonTouchDetails GetTouchDetail()
        {
            Random r = new Random();
            JsonTouchDetails objTD = new JsonTouchDetails();
            objTD.ClientX = r.Next(0, 600);
            objTD.ClientY = r.Next(0, 1280);
            objTD.Date = DateTime.Now.AddSeconds(-10).ToString();   //(-r.Next(0, 30)).ToString();
            Thread.Sleep(1000);
            objTD.Press = r.Next(1, 100);
            return objTD;
        }

        private JsonScrollDetails[] GetScrollDetails(JsonTouchDetails p_objStartTouch, JsonTouchDetails p_objEndTouch)
        {
            JsonScrollDetails objScrollDetails = new JsonScrollDetails();
            objScrollDetails.StartTouchData = p_objStartTouch;
            objScrollDetails.CloseTouchData =p_objEndTouch;

            var colScrollDetails = new JsonScrollDetails[]
            {
                objScrollDetails
            };
            return colScrollDetails;
        }

<<<<<<< .mine
        //[WebInvoke(UriTemplate = "Update/{id}", Method = "PUT")]
        //public bool Update(string id, IPackage instance)
        //{
        //    // TODO: Update the given instance of SampleItem in the collection
        //    throw new NotImplementedException();
        //}
=======
>>>>>>> .r513


     

        //PM : this method is not needed for FP

        #region old methods 
        //[WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        //public void Delete(string id)
        //{
        //    // TODO: Remove the instance of SampleItem with the given id from the collection
        //    throw new NotImplementedException();
        //}

        //[WebGet(UriTemplate = "")]
        //public List<SampleItem> GetCollection()
        //{
        //    // TODO: Replace the current implementation to return a collection of SampleItem instances
        //    return new List<SampleItem>() { new SampleItem() { Id = 1, StringValue = "Hello" } };
        //}


        //[WebGet(UriTemplate = "{id}")]
        //public SampleItem Get(string id)
        //{
        //    // TODO: Return the instance of SampleItem with the given id
        //    throw new NotImplementedException();
        //}

        //[WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        //public SampleItem Update(string id, SampleItem instance)
        //{
        //    // TODO: Update the given instance of SampleItem in the collection
        //    throw new NotImplementedException();
        //}

        #endregion oldmethods

    }
}

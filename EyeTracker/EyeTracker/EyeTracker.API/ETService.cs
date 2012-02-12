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

        

        [WebInvoke(UriTemplate = "", Method = "POST", 
                    RequestFormat=WebMessageFormat.Json, 
                    ResponseFormat=WebMessageFormat.Json)]
        public bool Post(IPackage package)
        {  
                //return ParseVisitEvents(mState, package);
                EventParser.Parse(package);
                return true;
        }

        

     

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


using System.Runtime.Serialization;

namespace EyeTracker.API.BL.Contract
{
    /// <summary>
    /// Package data - contains data received from client 
    /// represents client parameters and contains information about using sessions
    /// </summary>
    /// <example>
    /// {
    ///     cid:'0001-1000',   //  - string - application ID 
    ///     sh:100,            //  - int- device  screen details????
    ///     sw:100,            //  - int- device screen details????
    ///     ssd:               //session data
    ///     [
    ///     si{}, si{}, si{}......
    ///     ]
    /// }
    /// </example>
    [DataContract]
    public class JsonPackage : IPackage
    {
        [DataMember(Name = "cid")]
        public string ClientKey { get; set; }

        [DataMember(Name = "sh")]
        public int ScreenHeight { get; set; }

        [DataMember(Name = "sw")]
        public int ScreenWidth { get; set; }

        [DataMember(Name = "sd")]
        public JsonSessionInfo[] SessionsInfo { get; set; }

        #region IPackage Members

        public static string Indent
        {
            get { return "JsonPackage"; }
        }

        #endregion
    }

}
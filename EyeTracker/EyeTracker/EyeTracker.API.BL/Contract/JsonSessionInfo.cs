
using System.Runtime.Serialization;
namespace EyeTracker.API.BL.Contract
{

    /// <summary>
    /// Session Info object 
    /// contains infromation about one single using session 
    /// </summary>
    /// <example>
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </example>
    [DataContract]
    public class JsonSessionInfo : IPackage
    {

        /// <summary>
        /// uri
        /// </summary>
        [DataMember(Name = "uri")]
        public string PageUri { get; set; }

        /// <summary>
        /// cw
        /// </summary>
        [DataMember(Name = "cw")]
        public int ClientWidth { get; set; }

        /// <summary>
        /// ch
        /// </summary>
        [DataMember(Name = "ch")]
        public int ClientHeight { get; set; }

        /// <summary>
        /// ss
        /// </summary>
        [DataMember(Name = "ss")]
        public string SessionStartDate { get; set; }

        /// <summary>
        /// sc
        /// </summary>
        [DataMember(Name = "sc")]
        public string SessionCloseDate { get; set; }

        /// <summary>
        /// tda
        /// </summary>
        [DataMember(Name = "tda")]
        public JsonTouchDetails[] TouchDetails { get; set; }

        /// <summary>
        /// sda
        /// </summary>
        [DataMember(Name = "sda")]
        public JsonScrollDetails[] ScrollDetails { get; set; }

        /// <summary>
        /// vwa
        /// </summary>
        [DataMember(Name="vwa")]
        public JsonViewAreaDetails[] ViewAreaDetails { get; set; }

        #region IPackage Members

        public string Indent
        {
            get { return "JsonSessionInfo"; }
        }

        #endregion
    }
}
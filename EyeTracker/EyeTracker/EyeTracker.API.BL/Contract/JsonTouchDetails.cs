using System.Runtime.Serialization;


namespace EyeTracker.API.BL.Contract
{
    /// <summary>
    /// touch/click details iformation 
    /// </summary>
    /// <example>
    /// 
    /// </example>
    [DataContract]
    public class JsonTouchDetails : IPackage
    {
        /// <summary>
        /// date of touch/click
        /// </summary>
        [DataMember(Name = "d")]
        public string Date { get; set; }

        /// <summary>
        /// coordinate X
        /// </summary>
        [DataMember(Name = "cx")]
        public int ClientX { get; set; }

        /// <summary>
        /// Coordinate Y
        /// </summary>
        [DataMember(Name = "cy")]
        public int ClientY { get; set; }

        /// <summary>
        /// pressure
        /// </summary>
        [DataMember(Name = "p")]
        public int Press { get; set; }

        #region IPackage Members

        public string Indent
        {
            get { return "JsonTouchDetails"; }
        }

        #endregion
    }
}
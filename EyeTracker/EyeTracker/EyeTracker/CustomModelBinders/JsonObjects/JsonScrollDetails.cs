
using System.Runtime.Serialization;



namespace EyeTracker.CustomModelBinders
{

    /// <summary>
    /// scroll details infromation object 
    /// describes information about one scroll event - first touch : where a scroll begun 
    /// last touch : where a scroll ended
    /// </summary>
    [DataContract]
    public class JsonScrollDetails : IPackage
    {
        /// <summary>
        /// std
        /// </summary>
        [DataMember(Name = "std")]
        public JsonTouchDetails StartTouchData { get; set; }


        /// <summary>
        /// ctd
        /// </summary>
        [DataMember(Name = "ctd")]
        public JsonTouchDetails CloseTouchData { get; set; }
    }

}




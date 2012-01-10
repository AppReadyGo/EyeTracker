

using System.Runtime.Serialization;
namespace EyeTracker.CustomModelBinders
{

    /// <summary>
    /// view area iformation object
    /// </summary>
    [DataContract]
    public class JsonViewAreaDetails :IPackage
    {
        [DataMember(Name = "cx")]
        public int CoordX { get; set; }

        [DataMember(Name = "cy")]
        public int CoordY { get; set; }

        [DataMember(Name="sd")]
        public string StartDate { get; set; }

        [DataMember(Name="fd")]
        public string FinishDate { get; set; }
    }
}
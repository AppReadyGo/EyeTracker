using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace EyeTracker.Core
{
    public class GeoInformation
    {
        public string Country { get; set; }

        public string City { get; set; }
    }

    public interface IGeoIpService
    {
        GeoInformation GetGeoInformation(string ip);
    }

    /// <summary>
    /// http://api.easyjquery.com/ips/?ip=82.18.248.84
    /// {
    ///     "IP":"82.18.248.84",
    ///     "continentCode":"EU",
    ///     "continentName":"Europe",
    ///     "countryCode2":"GB",
    ///     "COUNTRY":"GB",
    ///     "countryCode3":"GBR",
    ///     "countryName":"United Kingdom",
    ///     "regionName":"London, City of",
    ///     "cityName":"London",
    ///     "cityLatitude":51.5002,
    ///     "cityLongitude":-0.1262,
    ///     "countryLatitude":54,
    ///     "countryLongitude":-4.5,
    ///     "localTimeZone":"Europe\/London",
    ///     "localTime":"18:56:24 pm"
    /// }
    /// </summary>
    public class EasyQueryIpService : IGeoIpService
    {
        [DataContract]
        public class EasyQueryGeoInformation : GeoInformation
        {
            [DataMember(Name = "IP")]
            public string IP { get; set; }
            [DataMember(Name = "continentCode")]
            public string ContinentCode { get; set; }
            [DataMember(Name = "continentName")]
            public string ContinentName { get; set; }
            [DataMember(Name = "countryCode2")]
            public string CountryCode2 { get; set; }
            [DataMember(Name = "COUNTRY")]
            public string Country { get; set; }
            [DataMember(Name = "countryCode3")]
            public string CountryCode3 { get; set; }
            [DataMember(Name = "countryName")]
            public string CountryName { get; set; }
            [DataMember(Name = "regionName")]
            public string RegionName { get; set; }
            [DataMember(Name = "cityName")]
            public string CityName { get; set; }
            [DataMember(Name = "cityLatitude")]
            public double CityLatitude { get; set; }
            [DataMember(Name = "cityLongitude")]
            public double CityLongitude { get; set; }
            [DataMember(Name = "localTimeZone")]
            public string LocalTimeZone { get; set; }
            [DataMember(Name = "localTime")]
            public string LocalTime { get; set; }
        }

        private readonly string URL = "http://api.easyjquery.com/ips/?ip={0}";

        public GeoInformation GetGeoInformation(string ip)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(string.Format(URL, ip));
            request.UserAgent = "Finger - Mobillify";
            request.Referer = "http://finger.mobillify.com/";

            string json = string.Empty;
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return ParseJson(json);
        }

        private EasyQueryGeoInformation ParseJson(string json)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(EasyQueryGeoInformation));
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                MemoryStream ms = new MemoryStream(bytes);
                return serializer.ReadObject(ms) as EasyQueryGeoInformation;
            }
            catch
            {
                return null;
            }
        }
    }
}

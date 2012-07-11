using System.Collections.Generic;

namespace EyeTracker.Domain.Model
{
    public class Country
    {
        public virtual int GeoId { get; set; }

        public virtual string Name { get; set; }

        public virtual short Code { get; set; }
        
        public virtual string ISOCode { get; set; }

        public virtual string NativeName { get; set; }

        public virtual int TimeZone { get; set; }

        public virtual float Latitude { get; set; }

        public virtual float Longitude { get; set; }

        public virtual IEnumerable<Region> Regions { get; set; }
    }

    public class Continent
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual IEnumerable<Country> Countries { get; set; }
    }

    public class Region
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IEnumerable<City> Cities { get; set; }
    }

    public class City
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual float Latitude { get; set; }

        public virtual float Longitude { get; set; }

        public virtual IEnumerable<GeoKey> Keys { get; set; }
   }

    public class GeoKey
    {
        public virtual int Id { get; set; }

        public virtual string Key { get; set; }
    }
}

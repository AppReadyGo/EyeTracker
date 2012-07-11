using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping
{
    public class CountryMapping : ClassMapping<Country>
    {
        public CountryMapping() 
        {
            Table("Countries");

            Id(p => p.GeoId, map => { });
            Property(x => x.Name, map =>
            {
                map.NotNullable(true);
                map.Length(70);
            });
            Property(x => x.Code, map => map.NotNullable(true));
            Property(x => x.ISOCode, map => map.Length(2));
            Property(x => x.NativeName, map => map.Length(70));
            Property(x => x.TimeZone, map => map.NotNullable(true));
            //Property(x => x.Latitude, map => map.NotNullable(true));
            //Property(x => x.Longitude, map => map.NotNullable(true));
            Set(p => p.Regions, map =>
            {
                map.Key(k => k.Column("CountryID"));
                map.Lazy(CollectionLazy.Lazy);
                map.Cascade(Cascade.All);
            }, prop => prop.OneToMany());
        }
    }

    public class ContinentMapping : ClassMapping<Continent>
    {
        public ContinentMapping()
        {
            Table("Continents");

            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(x => x.Name, map => { map.NotNullable(true); map.Length(70); });
            Property(x => x.Code, map => map.NotNullable(true));
            Set(p => p.Countries, map =>
            {
                map.Key(k => k.Column("ContinentID"));
                map.Lazy(CollectionLazy.Lazy);
                map.Cascade(Cascade.All);
            }, prop => prop.OneToMany());
        }
    }

    public class RegionMapping : ClassMapping<Region>
    {
        public RegionMapping()
        {
            Table("Regions");

            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(x => x.Name, map => { map.NotNullable(true); map.Length(70); });
            Set(p => p.Cities, map =>
            {
                map.Key(k => k.Column("RegionID"));
                map.Lazy(CollectionLazy.Lazy);
                map.Cascade(Cascade.All);
            }, prop => prop.OneToMany());
        }
    }

    public class CityMapping : ClassMapping<City>
    {
        public CityMapping()
        {
            Table("Cities");

            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(x => x.Name, map => { map.NotNullable(true); map.Length(70); });
            Set(p => p.Keys, map => 
            { 
                map.Key(k => k.Column("CityID"));
                map.Lazy(CollectionLazy.Lazy);
                map.Cascade(Cascade.All); 
            }, prop => prop.OneToMany());
        }
    }

    public class GeoKeyMapping : ClassMapping<GeoKey>
    {
        public GeoKeyMapping()
        {
            Table("GeoKeys");

            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(x => x.Key, map => { map.NotNullable(true); map.Length(70); });
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class CountryMapping : ClassMapping<Country>
    {
        public CountryMapping() 
            : base()
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
        }
    }
}

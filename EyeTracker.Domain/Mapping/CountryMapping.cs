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
        {
            Table("[Country]");
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            Property(p => p.Name, map =>
            {
                map.Length(50);
                map.NotNullable(true);
            });
            Property(x => x.TimeZone, map => map.NotNullable(true));
        }
    }
}

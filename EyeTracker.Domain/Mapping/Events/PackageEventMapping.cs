using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;
using EyeTracker.Domain.Model.Events;

namespace EyeTracker.Domain.Mapping
{
    public class PackageEventMapping : ClassMapping<PackageEvent>
    {
        public PackageEventMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Key, map =>
            {
                map.Length(225);
                map.NotNullable(true);
            });
            Property(p => p.ScreenWidth, map => map.NotNullable(true));
            Property(p => p.ScreenHeight, map => map.NotNullable(true));
            Bag(p => p.Sessions, map =>
            {
                map.Cascade(Cascade.All);
                map.Key(k => k.Column("PackageEventId"));
                map.Lazy(CollectionLazy.Lazy);
            }, prop => prop.OneToMany());
        }
    }
}

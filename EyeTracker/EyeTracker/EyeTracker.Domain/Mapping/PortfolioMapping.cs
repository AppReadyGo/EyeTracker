using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class PortfolioMapping : ClassMapping<Portfolio>
    {
        public PortfolioMapping()
        {
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            Property(p => p.Description, map => {
                map.Length(255);
                map.NotNullable(true);
            });
            Property(x => x.CreateDate, map => map.NotNullable(true));
            ManyToOne(p => p.User, map =>
            {
                map.Column("UserId");
                map.NotNullable(true);
            });
            Property(x => x.TimeZone, map => map.NotNullable(true));
            Set(x => x.Applications, map =>
            {
                map.Key(k => k.Column("PortfolioId"));
                map.Lazy(CollectionLazy.NoLazy);
                map.Cascade(Cascade.DeleteOrphans);
                map.Access(Accessor.Field);
                map.Inverse(true);
            }, r => r.OneToMany());
        }
    }
}

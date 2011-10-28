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
            Bag(x => x.Applications, map =>
            {
                map.Table("PortfolioApplication");
                map.Key(k => k.Column("PortfolioId"));
                map.Cascade(Cascade.All);
                map.Lazy(CollectionLazy.NoLazy);
            }, r => r.OneToMany());
        }
    }
}

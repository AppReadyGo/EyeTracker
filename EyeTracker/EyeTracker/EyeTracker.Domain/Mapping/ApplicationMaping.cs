using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class ApplicationMaping : ClassMapping<Application>
    {
        public ApplicationMaping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Description, map =>
            {
                map.Length(225);
                map.NotNullable(true);
            });
            Property(x => x.Type, map => map.NotNullable(true));
            Property(x => x.CreateDate, map => map.NotNullable(true));
            ManyToOne(x => x.Portfolio, map =>
            {
                map.NotNullable(true);
                map.Column("PortfolioId");
                map.Lazy(LazyRelation.NoLazy);
            });
            Set(p => p.Screens, 
                map => {
                    map.Key(k => k.Column("ApplicationId"));
                    map.Lazy(CollectionLazy.NoLazy);
                    map.Table("ApplicationScreens");
                    map.Cascade(Cascade.All);
                },
                rel => rel.OneToMany() );
        }
    }
}

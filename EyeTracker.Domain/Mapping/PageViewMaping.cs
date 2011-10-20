using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class PageViewMaping : ClassMapping<PageView>
    {
        public PageViewMaping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Path, map =>
            {
                map.Length(225);
                map.NotNullable(true);
            });
            Property(x => x.IP, map =>
            {
                map.Length(15);
                map.NotNullable(true);
            });
            //ManyToOne(p => p.OSLanguage, map =>
            //{
            //    map.Lazy(LazyRelation.NoLazy);
            //    map.Column("OSLanguageId");
            //});
            ManyToOne(p => p.Country, map =>
            {
                map.Lazy(LazyRelation.NoLazy);
                map.Column("CountryId");
            });
        }
    }
}

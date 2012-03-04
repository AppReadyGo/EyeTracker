using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class ContentPageMaping : ClassMapping<ContentPage>
    {
        public ContentPageMaping()
        {
            Table("ContentPages");
            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(p => p.Title, map => { map.NotNullable(true); map.Length(50); });
            Property(p => p.Content, map => { map.NotNullable(true); });
            Property(p => p.Path, map => { map.NotNullable(true); map.Length(255); });
        }
    }
}

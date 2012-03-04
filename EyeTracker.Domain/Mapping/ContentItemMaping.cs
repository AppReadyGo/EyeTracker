using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class ContentItemMaping : ClassMapping<ContentItem>
    {
        public ContentItemMaping()
        {
            Table("Content");
            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(p => p.Key, map => { map.NotNullable(true); map.Length(50); });
            Property(p => p.SubKey, map => { map.NotNullable(true); map.Length(50); });
            Property(p => p.Value, map => { map.NotNullable(true); map.Column(z => z.SqlType("nvarchar(max)")); });
        }
    }
}

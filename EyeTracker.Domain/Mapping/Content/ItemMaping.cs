using EyeTracker.Domain.Model.Content;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.Content
{
    public class ItemMaping : ClassMapping<Item>
    {
        public ItemMaping()
        {
            Schema("cont");
            Table("Items");
            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(p => p.Key, map => { map.NotNullable(true); map.Length(50); });
            Property(p => p.SubKey, map => { map.NotNullable(true); map.Length(50); });
            Property(p => p.Value, map => { map.NotNullable(true); map.Column(z => z.SqlType("nvarchar(max)")); });
        }
    }
}

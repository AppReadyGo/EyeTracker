using EyeTracker.Domain.Model.Content;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.Content
{
    public class ItemMapping : ClassMapping<Item>
    {
        public ItemMapping()
        {
            Schema("cont");
            Table("Items");

            Id(x => x.Id, map => { map.Column("ID"); map.Generator(Generators.Identity); });
            Property(x => x.SubKey, map => { map.NotNullable(true); map.Length(256); });
            Property(x => x.IsHTML, map => { map.NotNullable(true); });
            Property(x => x.Value, map => { map.NotNullable(true); });
            ManyToOne(x => x.Page, map => { map.Column("PageID"); });
            ManyToOne(x => x.Mail, map => { map.Column("MailID"); });
            ManyToOne(x => x.Key, map => { map.Column("KeyID"); });
        }
    }
}

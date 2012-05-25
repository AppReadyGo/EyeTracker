using EyeTracker.Domain.Model.Content;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.Content
{
    public class KeyMapping : ClassMapping<Key>
    {
        public KeyMapping()
        {
            Schema("cont");
            Table("Keys");

            Id(x => x.Id, map => { map.Column("ID"); map.Generator(Generators.Identity); });
            Property(x => x.Url, map => { map.NotNullable(true); map.Access(Accessor.ReadOnly); map.Insert(false); map.Update(false); });
            Set(
                x => x.Items,
                map =>
                {
                    map.Access(Accessor.Field);
                    map.Cascade(Cascade.All);
                    map.Inverse(true);
                    map.Key(x => x.Column("KeyID"));
                },
                r => r.OneToMany());
        }
    }
}

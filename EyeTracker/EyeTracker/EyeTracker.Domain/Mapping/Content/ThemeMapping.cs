using EyeTracker.Domain.Model.Content;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.Content
{
    public class ThemeMapping : ClassMapping<Theme>
    {
        public ThemeMapping()
        {
            Schema("cont");
            Table("Themes");

            Id(x => x.Id, map => { map.Column("ID"); map.Generator(Generators.Identity); });
            Property(x => x.Name, map => { map.NotNullable(true); map.Length(256); });
            Property(x => x.Url, map => { map.NotNullable(true); map.Access(Accessor.ReadOnly); map.Insert(false); map.Update(false); });
            Property(x => x.Type, map => { map.NotNullable(true); map.Access(Accessor.ReadOnly); map.Insert(false); map.Update(false); });
        }
    }
}

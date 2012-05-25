using EyeTracker.Domain.Model.Content;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.Content
{
    public class PageMapping : ClassMapping<Page>
    {
        public PageMapping()
        {
            Schema("cont");
            Table("Pages");

            Id(x => x.Id, map => { map.Column("ID"); map.Generator(Generators.Identity); });
            Property(x => x.Url, map => { map.NotNullable(true); map.Length(256); });
            Property(x => x.Theme, map => { map.NotNullable(true); map.Column("ThemeID"); });
            OneToOne(x => x.Title, map => { });
            OneToOne(x => x.Content, map => { });
        }
    }
}

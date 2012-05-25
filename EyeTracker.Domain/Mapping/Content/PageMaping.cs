using EyeTracker.Domain.Model.Content;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.Content
{
    /*
    public class PageMaping : ClassMapping<Page>
    {
        public PageMaping()
        {
            Schema("cont");
            Table("Pages");
            DiscriminatorValue((byte)0);
            Discriminator(map => { map.Column("TypeID"); });

            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(x => x.Type, x => { x.NotNullable(true); x.Column("PageTypeID"); x.Access(Accessor.ReadOnly); x.Insert(false); x.Update(false); });
            Property(p => p.Title, map => { map.NotNullable(true); map.Length(50); });
            Property(p => p.Content, map => { map.NotNullable(true); });
            Property(p => p.Path, map => { map.NotNullable(true); map.Length(255); });
        }
    }

    public class BasePageMapping : SubclassMapping<BasePage>
    {
        public BasePageMapping()
        {
            DiscriminatorValue((byte)PageType.Base);
        }
    }
     */
}

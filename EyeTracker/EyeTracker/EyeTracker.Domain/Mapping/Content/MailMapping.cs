using EyeTracker.Domain.Model.Content;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.Content
{
    public class SystemMailMapping : ClassMapping<SystemMail>
    {
        public SystemMailMapping()
        {
            Schema("cont");
            Table("Mails");

            Discriminator(map => map.Column("IsSystem"));
            DiscriminatorValue(true);
            Property(p => p.IsSystem, p => { p.NotNullable(true); p.Column("IsSystem"); p.Access(Accessor.ReadOnly); p.Insert(false); p.Update(false); });

            Id(x => x.Id, map => { map.Column("ID"); map.Generator(Generators.Identity); });
            Property(x => x.Url, map => { map.NotNullable(true); map.Length(256); });
            Property(x => x.Theme, map => { map.NotNullable(true); map.Column("ThemeID"); });
            Property(x => x.IsSystem, map => { map.NotNullable(true); });
            OneToOne(x => x.Subject, map => { });
            OneToOne(x => x.Body, map => { });
        }
    }
    public class MailMapping : SubclassMapping<Mail>
    {
        public MailMapping()
        {
            DiscriminatorValue(false);
        }
    }

}

using EyeTracker.Domain.Model.Content;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.Content
{
    public class EmailMaping : ClassMapping<Email>
    {
        public EmailMaping()
        {
            Schema("cont");
            Table("Emails");
            DiscriminatorValue((byte)0);
            Discriminator(map => { map.Column("EmailTypeID"); });

            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(x => x.Type, x => { x.NotNullable(true); x.Column("EmailTypeID"); x.Access(Accessor.ReadOnly); x.Insert(false); x.Update(false); });
            Property(p => p.Subject, map => { map.NotNullable(true); map.Length(255); });
            Property(p => p.Body, map => { map.NotNullable(true); });
        }
    }

    public class BaseEmailMapping : SubclassMapping<BaseEmail>
    {
        public BaseEmailMapping()
        {
            DiscriminatorValue((byte)EmailType.Base);
        }
    }
}

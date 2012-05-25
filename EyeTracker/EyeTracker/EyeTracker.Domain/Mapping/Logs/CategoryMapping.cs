using EyeTracker.Domain.Model.Logs;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.Logs
{
    public class CategoryMapping : ClassMapping<Category>
    {
        public CategoryMapping()
        {
            Schema("log");
            Table("Category");
            Id(x => x.Id, map => { map.Column("ID"); map.Generator(Generators.Identity); });
            Property(x => x.Name, map => { map.Length(64); map.NotNullable(false); });
        }
    }
}

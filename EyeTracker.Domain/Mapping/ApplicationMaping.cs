using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class ApplicationMaping : ClassMapping<Application>
    {
        public ApplicationMaping()
        {
            Id(x => x.Id, map => map.Column("UserApplicationId"));
            Property(x => x.Description, map =>
            {
                map.Length(225);
                map.NotNullable(true);
            });
            Property(x => x.Type, map => map.NotNullable(true));
            Property(x => x.CreateDate, map => map.NotNullable(true));
            Bag(x => x.Portfolios, map =>
            {
                map.Table("PortfolioApplication");
                map.Key(k => k.Column("ApplicationId"));
                map.Cascade(Cascade.All);
            }, r => r.ManyToMany(mmp => mmp.Column("PortfolioId")));
        }
    }
}

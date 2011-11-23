using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class ViewPartMaping : ClassMapping<ViewPart>
    {
        public ViewPartMaping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(p => p.Date, map => map.NotNullable(true));
            Property(p => p.X, map => map.NotNullable(true));
            Property(p => p.Y, map => map.NotNullable(true));
            ManyToOne(p => p.PageView, map =>
            {
                map.NotNullable(true);
                map.Column("PageViewId");
                map.Cascade(Cascade.All);
            });
        }
    }
}

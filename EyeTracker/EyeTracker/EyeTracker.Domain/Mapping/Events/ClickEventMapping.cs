using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;
using EyeTracker.Domain.Model.Events;

namespace EyeTracker.Domain.Mapping
{
    public class ClickEventMapping : ClassMapping<ClickEvent>
    {
        public ClickEventMapping()
        {
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            Property(p => p.Date, map => map.NotNullable(true));
            Property(p => p.ClientX, map => map.NotNullable(true));
            Property(p => p.ClientY, map => map.NotNullable(true));
            Property(p => p.VisitInfoId, map => map.NotNullable(true));
            Property(p => p.VisitInfoId, map => map.NotNullable(true));
            ManyToOne(p => p.SessionInfoEvent, map =>
            {
                map.NotNullable(false);
                map.Column("SessionInfoEventId");
                map.Cascade(Cascade.All);
            });
        }
    }
}

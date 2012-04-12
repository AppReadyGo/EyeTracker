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
    public class ViewPartEventMapping : ClassMapping<ViewPartEvent>
    {
        public ViewPartEventMapping()
        {
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            Property(p => p.StartDate, map => map.NotNullable(true));
            Property(p => p.FinishDate, map => map.NotNullable(true));
            Property(p => p.ScrollTop, map => map.NotNullable(true));
            Property(p => p.ScrollLeft, map => map.NotNullable(true));
            Property(p => p.TimeSpan, map => map.NotNullable(true));
            Property(p => p.VisitInfoId, map => map.NotNullable(true));
            Property(p => p.Orientation, map => map.NotNullable(true));
            ManyToOne(p => p.SessionInfoEvent, map =>
            {
                map.Cascade(Cascade.All);
                map.NotNullable(false);
                map.Column("SessionInfoEventId");
            });
        }
    }
}

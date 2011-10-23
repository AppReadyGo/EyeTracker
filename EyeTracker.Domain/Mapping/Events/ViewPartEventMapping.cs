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
            Property(p => p.Date, map => map.NotNullable(true));
            Property(p => p.ScrollTop, map => map.NotNullable(true));
            Property(p => p.ScrollLeft, map => map.NotNullable(true));
            Property(p => p.TimeSpan, map => map.NotNullable(true));
            Property(p => p.VisitInfoId, map => map.NotNullable(true));
        }
    }
}

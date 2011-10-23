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
    public class VisitEventMapping : ClassMapping<VisitEvent>
    {
        public VisitEventMapping()
        {
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            Property(p => p.Key, map => {
                map.NotNullable(true);
                map.Length(13);
            });
            Property(p => p.Date, map => map.NotNullable(true));
            Property(p => p.Path, map =>
            {
                map.NotNullable(true);
                map.Length(256);
            });
            Property(p => p.PreviousVisitId, map => { });
            Property(p => p.Ip, map => map.Length(15));
            Property(p => p.Language, map => map.Length(50));
            Property(p => p.OS, map => map.Length(150));
            Property(p => p.Browser, map => map.Length(150));
            Property(p => p.ScreenWidth, map => map.NotNullable(true));
            Property(p => p.ScreenHeight, map => map.NotNullable(true));
            Property(p => p.ClientWidth, map => map.NotNullable(true));
            Property(p => p.ClientHeight, map => map.NotNullable(true));
        }
    }
}

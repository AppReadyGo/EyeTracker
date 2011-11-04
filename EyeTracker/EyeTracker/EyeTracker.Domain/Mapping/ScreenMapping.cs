using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class ScreenMapping : ClassMapping<Screen>
    {
        public ScreenMapping()
        {
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            Property(p => p.ApplicationId, map => map.NotNullable(true));
            Property(p => p.Height, map => map.NotNullable(true));
            Property(p => p.Width, map => map.NotNullable(true));
        }
    }
}

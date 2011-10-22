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
    public class PackageEventMapping : ClassMapping<PackageEvent>
    {
        public PackageEventMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
        }
    }
}

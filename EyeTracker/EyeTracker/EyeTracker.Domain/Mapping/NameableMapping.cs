using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class NameableMapping<T> : ClassMapping<T> where T : Nameable
    {
        public NameableMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Name, map => {
                map.Length(100);
                map.NotNullable(true);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class OperationSystemMapping : NameableMapping<OperationSystem>
    {
        static OperationSystemMapping()
        {
            int i = 5;
        }
        //public OperationSystemMapping()
        //{
        //    Id(x => x.Id, map => map.Generator(Generators.Identity));
        //    Property(x => x.Name, map =>
        //    {
        //        map.Length(100);
        //        map.NotNullable(true);
        //    });
        //}
    }
}

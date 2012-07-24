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

            ManyToOne(p => p.Application, map =>
            {
                map.NotNullable(true);
                map.Column("ApplicationID");
                map.Cascade(Cascade.All);
            }); 
            
            Property(p => p.Path, map => { map.NotNullable(true); map.Length(256); });
            Property(p => p.Height, map => map.NotNullable(true));
            Property(p => p.Width, map => map.NotNullable(true));
            Property(p => p.FileExtension, map => { map.NotNullable(true); map.Length(5); });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class ApplicationMaping : ClassMapping<Application>
    {
        public ApplicationMaping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Description, map =>
            {
                map.Length(225);
                map.NotNullable(true);
            });
            Property(x => x.Type, map => map.NotNullable(true));
            Property(x => x.CreateDate, map => map.NotNullable(true));
            ManyToOne(p => p.User, map =>
            {
                map.Column("UserID");
                map.NotNullable(true);
            });

            Set(p => p.Screens,
                map =>
                {
                    map.Key(k => k.Column("ApplicationID"));
                    map.Table("Screen");
                    map.Inverse(true);
                    map.Access(Accessor.Field);
                    map.Cascade(Cascade.All);
                },
                rel => rel.OneToMany());

            ManyToOne(p => p.Package, map =>
            {
                map.Column("PackageID");
            });
        }
    }
}

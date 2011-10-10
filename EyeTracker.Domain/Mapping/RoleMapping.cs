using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class RoleMapping : ClassMapping<Role>
    {
        public RoleMapping()
        {
            Table("aspnet_Roles");
            Id(x => x.Id, map => map.Column("RoleId"));
            Property(x => x.Name, map =>
            {
                map.Column("RoleName");
                map.Length(255);
                map.NotNullable(true);
            });
            Property(x => x.Description, map => map.Length(255));
            ManyToOne(p => p.App, map =>
            {
                map.Lazy(LazyRelation.NoLazy);
                map.Column("ApplicationId");
            });
            Bag(x => x.Users, map =>
            {
                map.Table("aspnet_UsersInRoles");
                map.Key(k => k.Column("RoleId"));
            }, r => r.ManyToMany(mmp => mmp.Column("UserId")));
        }
    }
}

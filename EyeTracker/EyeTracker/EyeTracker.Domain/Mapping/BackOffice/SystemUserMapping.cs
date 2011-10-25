using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Mapping.BackOffice
{
    public class SystemUserMapping : ClassMapping<SystemUser>
    {
        public SystemUserMapping()
        {
            Table("aspnet_Users");
            Id(x => x.Id, map => map.Column("UserId"));
            Property(x => x.Name, map =>
            {
                map.Column("UserName");
                map.Length(255);
                map.NotNullable(true);
            });
            Property(x => x.LastActivityDate, map => { });
            OneToOne(x => x.Membership, map => map.Lazy(LazyRelation.NoLazy));
            //Join("aspnet_Membership", map =>
            //{
            //    map.Key(key => key.Column("UserId"));
            //    map.Property(x => x.Email);
            //});
            //Join("UserProfiler", map =>
            //{
            //    map.Key(key => key.Column("UserId"));
            //    map.Property(x => x.TimeZone);
            //});
            ManyToOne(p => p.Application, map =>
            {
                map.Lazy(LazyRelation.NoLazy);
                map.Column("ApplicationId");
            });
            //ManyToOne(p => p.Profiler, map =>
            //{
            //    map.NotNullable(true);
            //    map.Column("ProfilerId");
            //});
            Bag(x => x.Roles, map =>
            {
                map.Table("aspnet_UsersInRoles");
                map.Key(k => k.Column("UserId"));
                map.Cascade(Cascade.All);
            }, r => r.ManyToMany(mmp => mmp.Column("RoleId")));
            SchemaAction(NHibernate.Mapping.ByCode.SchemaAction.None);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Mapping.BackOffice
{
    public class SystemMembershipMapping : ClassMapping<SystemMembership>
    {
        public SystemMembershipMapping()
        {
            Table("aspnet_Membership");
            Property(x => x.Email, map =>
            {
                map.Length(255);
                map.NotNullable(true);
            });
            SchemaAction(NHibernate.Mapping.ByCode.SchemaAction.None);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;

namespace EyeTracker.Domain.Mapping
{
    public class UserApplicationMaping : ClassMapping<UserApplication>
    {
        public UserApplicationMaping()
        {
            Id(x => x.Id, map => map.Column("UserApplicationId"));
            Property(x => x.Description, map =>
            {
                map.Length(225);
                map.NotNullable(true);
            });
            Property(x => x.CreateDate, map => map.NotNullable(true));
            ManyToOne(p => p.User, map =>
            {
                map.Column("UserId");
            });
        }
    }
}

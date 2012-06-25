﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.Events;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping.Events
{
    public class ScrollEventMapping : ClassMapping<ScrollEvent>
    {
        public ScrollEventMapping()
        {
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            ManyToOne(p => p.FirstTouch, map =>
            {
                map.Cascade(Cascade.All);
                map.NotNullable(true);
                map.Column("FirstTouchId");
            });
            ManyToOne(p => p.LastTouch, map =>
            {
                map.Cascade(Cascade.All);
                map.NotNullable(true);
                map.Column("LastTouchId");
            });
            //Property(p => p.FirstTouchId, map => map.NotNullable(true));
            //Property(p => p.LastTouchId, map => map.NotNullable(true));
            ManyToOne(p => p.SessionInfoEvent, map =>
            {
                map.Cascade(Cascade.All);
                map.NotNullable(false);
                map.Column("SessionInfoEventId");
            });
        }
    }
}
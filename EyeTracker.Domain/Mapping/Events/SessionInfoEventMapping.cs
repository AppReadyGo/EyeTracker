﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model.Events;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping.Events
{
    public class SessionInfoEventMapping : ClassMapping<SessionInfoEvent>
    {
        public SessionInfoEventMapping()
        {
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Path, map =>
            {
                map.Length(225);
                map.NotNullable(true);
            });
            Property(p => p.ClientWidth, map => map.NotNullable(true));
            Property(p => p.ClientHeight, map => map.NotNullable(true));
            Property(p => p.StartDate, map => map.NotNullable(true));
            Property(p => p.CloseDate, map => map.NotNullable(true));
            Bag(p => p.Clicks, map =>
            {
                map.Cascade(Cascade.All);
                map.Key(k => k.Column("SessionInfoEventId"));
                map.Lazy(CollectionLazy.Lazy);
            }, prop => prop.OneToMany());
            Bag(p => p.Scrolls, map =>
            {
                map.Cascade(Cascade.All); //!!
                map.Key(k => k.Column("SessionInfoEventId"));
                map.Lazy(CollectionLazy.Lazy);
            }, prop => prop.OneToMany());
            Bag(p => p.ScreenViewParts, map =>
            {
                map.Cascade(Cascade.All);
                map.Key(k => k.Column("SessionInfoEventId"));
                map.Lazy(CollectionLazy.Lazy);
            }, prop => prop.OneToMany());

            ManyToOne(p => p.PackageEvent, map =>
            {
                map.Cascade(Cascade.All);
                map.NotNullable(true);
                map.Column("PackageEventId");
                
            });
        }

    }
}
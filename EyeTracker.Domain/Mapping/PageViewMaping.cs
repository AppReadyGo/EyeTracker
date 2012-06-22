using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class PageViewMaping : ClassMapping<PageView>
    {
        public PageViewMaping()
        {
            Id(x => x.Id, map => { map.Generator(Generators.Identity); });
            Property(p => p.Date, map => map.NotNullable(true));
            OneToOne(p => p.PreviousPageView, map => { });
            Property(x => x.Path, map =>
            {
                map.Length(256);
                map.NotNullable(true);
            });
            Property(p => p.Ip, map => map.Length(15));
            ManyToOne(p => p.Language, map =>
            {
                map.Lazy(LazyRelation.NoLazy);
                map.Column("LanguageId");
            });
            ManyToOne(p => p.Country, map =>
            {
                map.Lazy(LazyRelation.NoLazy);
                map.Column("CountryId");
            });
            Property(p => p.City, map => map.Length(50));
            ManyToOne(p => p.OperationSystem, map =>
            {
                map.Lazy(LazyRelation.NoLazy);
                map.Column("OperationSystemId");
            });
            ManyToOne(p => p.Browser, map =>
            {
                map.Lazy(LazyRelation.NoLazy);
                map.Column("BrowserId");
            });
            Property(p => p.ScreenWidth, map => map.NotNullable(true));
            Property(p => p.ScreenHeight, map => map.NotNullable(true));
            Property(p => p.ClientWidth, map => map.NotNullable(true));
            Property(p => p.ClientHeight, map => map.NotNullable(true));
            ManyToOne(p => p.Application, map =>
            {
                map.NotNullable(true);
                map.Lazy(LazyRelation.NoLazy);
                map.Column("ApplicationId");
                map.Cascade(Cascade.All);
            });
            Bag(p => p.Clicks, map =>
            {
                map.Key(k => k.Column("PageViewId"));
                map.Lazy(CollectionLazy.Lazy);
                map.Cascade(Cascade.All);
            }, prop => prop.OneToMany());
            //Bag(p => p.ViewParts, map =>
            //{
            //    map.Key(k => k.Column("PageViewId"));
            //    map.Lazy(CollectionLazy.Lazy);
            //    map.Cascade(Cascade.All);
            //}, prop => prop.OneToMany());
        }
    }
}

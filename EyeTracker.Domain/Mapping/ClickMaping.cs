﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using EyeTracker.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace EyeTracker.Domain.Mapping
{
    public class ClickMaping : ClassMapping<Click>
    {
        public ClickMaping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
        }
    }
}
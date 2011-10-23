using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class Country
    {
        public virtual int GeoId { get; set; }

        public virtual string Name { get; set; }

        public virtual short Code { get; set; }
        
        public virtual string ISOCode { get; set; }

        public virtual string NativeName { get; set; }

        public virtual int TimeZone { get; set; }
    }
}

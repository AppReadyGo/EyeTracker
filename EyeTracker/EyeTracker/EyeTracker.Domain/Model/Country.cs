using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class Country
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int TimeZone { get; set; }
    }
}

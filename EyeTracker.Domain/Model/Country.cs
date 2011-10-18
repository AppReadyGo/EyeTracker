using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeZone { get; set; }
    }
}

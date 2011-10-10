using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class Entity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
    }
}

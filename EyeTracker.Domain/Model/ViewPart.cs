using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class ViewPart
    {
        public virtual long Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual PageView PageView { get; set; }
    }
}

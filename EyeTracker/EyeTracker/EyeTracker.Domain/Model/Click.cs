using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    /// <summary>
    /// this class represents Click data model 
    /// each instance of this class contains base information about one single click (touch)
    /// </summary>
    public class Click
    {
        public virtual long Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual PageView PageView { get; set; }
    }
}

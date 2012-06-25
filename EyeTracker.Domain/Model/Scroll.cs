using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    /// <summary>
    /// this class represents scrolling data
    /// </summary>
    public class Scroll
    {

        /// <summary>
        /// Schroll ID
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// Start scrolling property
        /// </summary>
        public virtual Click FirstTouch { get; set; }

        /// <summary>
        /// Finish scrolling property
        /// </summary>
        public virtual Click LastTouch { get; set; }


        /// <summary>
        /// Page View ID
        /// </summary>
        public virtual PageView PageView { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    /// <summary>
    /// Enum represents Application platform types
    /// </summary>
    public enum ApplicationType
    {
        /// <summary>
        /// web application
        /// </summary>
        Web = 1,
        /// <summary>
        /// webmobile application
        /// </summary>
        WebMobile,
        /// <summary>
        /// android os based application
        /// </summary>
        Android,
        /// <summary>
        /// iOS based applications
        /// </summary>
        iPhone,
        /// <summary>
        /// windows mobile based application
        /// </summary>
        WindowsMobile
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands
{
    public static class Extentions
    {
        public static bool CheckLength(this string value, int min, int? max = null)
        {
            return value.Length >= min && (!max.HasValue || value.Length <= max);
        }
    }
}

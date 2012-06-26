using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common
{
    public static class Extensions
    {
        public static DateTime StartDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        public static DateTime EndDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
    }
}

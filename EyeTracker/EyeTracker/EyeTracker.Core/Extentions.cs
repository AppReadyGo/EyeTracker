using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Core
{
    public static class Extentions
    {
        public static double MilliTimeStamp(this DateTime date)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = date.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);

            return ts.TotalMilliseconds;
        }


        /// <summary>
        /// Merge point with distance less than mergeDist and if the point Intensity more than minIntensity
        /// </summary>
        /// <param name="points"></param>
        /// <param name="mergeDist"></param>
        /// <param name="minIntensity"></param>
        /// <returns></returns>
        public static List<IntensityPoint> Merge(this List<IntensityPoint> points, int mergeDist, int minIntensity = 1)
        {
            //Get max axis distance
            int maxDist = (int)Math.Sqrt((mergeDist ^ 2) / 2);
            for (int i = 1; i < points.Count; i++)
            {
                var curClick = points[i];
                if (curClick.Intensity <= minIntensity)
                {
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (j != i)
                        {
                            var compareClick = points[j];
                            if (compareClick.Intensity > 0)
                            {
                                int xDist = Math.Abs(curClick.X - compareClick.X);
                                int yDist = Math.Abs(curClick.Y - compareClick.Y);
                                int distance = (int)Math.Sqrt((xDist * xDist) + (yDist * yDist));
                                if (distance <= mergeDist)
                                {
                                    compareClick.Intensity += curClick.Intensity;
                                    curClick.Intensity = 0;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return points.Where(curClick => curClick.Intensity > 0).ToList();
        }

        public static List<IntensityLine> Merge(this List<IntensityLine> clicksList, int mergeDist, int minIntensity = 1)
        {
            //Get max axis distance
            int maxDist = (int)Math.Sqrt((mergeDist ^ 2) / 2);
            for (int i = 1; i < clicksList.Count; i++)
            {
                var curClick = clicksList[i];
                if (curClick.Intensity <= minIntensity)
                {
                    for (int j = 0; j < clicksList.Count; j++)
                    {
                        if (j != i)
                        {
                            var compareClick = clicksList[j];
                            if (compareClick.Intensity > 0)
                            {
                                int xDist = Math.Abs(curClick.X - compareClick.X);
                                int yDist = Math.Abs(curClick.Y - compareClick.Y);
                                int distance = (int)Math.Sqrt((xDist * xDist) + (yDist * yDist));
                                if (distance <= mergeDist)
                                {
                                    compareClick.Intensity += curClick.Intensity;
                                    curClick.Intensity = 0;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return clicksList.Where(curClick => curClick.Intensity > 0).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Analytics;
using System.Drawing;

namespace EyeTracker.Common.Queries.Analytics
{
    public class ClickHeatMapDataQuery : IQuery<IEnumerable<ClickHeatMapDataResult>>
    {
        public long AplicationId { get; private set; }
        public string Path { get; private set; }
        public Size ScreenSize { get; private set; }
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }

        public ClickHeatMapDataQuery(long appId, string path, Size screenSize, DateTime fromDate, DateTime toDate)
        {
            this.AplicationId = appId;
            this.Path = path;
            this.ScreenSize = screenSize;
            this.FromDate = fromDate.StartDay();
            this.ToDate = toDate.EndDay();
        }
    }
}

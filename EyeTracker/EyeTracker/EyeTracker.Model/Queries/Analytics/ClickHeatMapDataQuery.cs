using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Analytics;

namespace EyeTracker.Common.Queries.Analytics
{
    public class ClickHeatMapDataQuery : IQuery<IEnumerable<ClickHeatMapDataResult>>
    {
        public long AplicationId { get; private set; }
        public string Path { get; private set; }
        public int ClientWidth { get; private set; }
        public int ClientHeight { get; private set; }
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }

        public ClickHeatMapDataQuery(long appId, string path, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            this.AplicationId = appId;
            this.Path = path;
            this.ClientHeight = clientHeight;
            this.ClientWidth = clientWidth;
            this.FromDate = fromDate;
            this.ToDate = toDate;
        }
    }
}

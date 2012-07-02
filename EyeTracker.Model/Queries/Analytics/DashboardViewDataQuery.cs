using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using System.Drawing;

namespace EyeTracker.Common.Queries.Analytics
{
    public class DashboardViewDataQuery : IQuery<DashboardViewDataResult>
    {
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public int Portfolio { get; private set; }
        public IEnumerable<int> Applications { get; private set; }
        public IEnumerable<Size> ScreenSizes { get; private set; }
        public IEnumerable<string> Pathes { get; private set; }
        public string Language { get; private set; }
        public string OperatingSystem { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public DataGrouping DataGrouping { get; private set; }

        public DashboardViewDataQuery(
            DateTime from,
            DateTime to,
            int portfolio,
            IEnumerable<int> applications,
            IEnumerable<Size> screenSizes,
            IEnumerable<string> pathes,
            string language,
            string operatingSystem,
            string country,
            string city,
            DataGrouping dataGrouping)
        {
            this.From = from;
            this.To = to;
            this.Portfolio = portfolio;
            this.Applications = applications;
            this.ScreenSizes = screenSizes;
            this.Pathes = pathes;
            this.Language = language;
            this.OperatingSystem = operatingSystem;
            this.Country = country;
            this.City = city;
            this.DataGrouping = dataGrouping;
        }
    }
}

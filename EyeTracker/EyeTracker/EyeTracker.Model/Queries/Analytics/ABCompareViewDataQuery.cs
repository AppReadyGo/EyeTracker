using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;
using System.Drawing;

namespace EyeTracker.Common.Queries.Analytics
{
    public class ABCompareViewDataQuery : IQuery<ABCompareViewDataResult>, IFilterQuery
    {
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public int ApplicationId { get; private set; }
        public Size? ScreenSize { get; private set; }
        public string Path { get; private set; }
        public string SecondPath { get; private set; }
        public string Language { get; private set; }
        public string OperatingSystem { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }

        public ABCompareViewDataQuery(
            DateTime from,
            DateTime to,
            int applicationId,
            Size? screenSize,
            string path,
            string secondPath,
            string language,
            string operatingSystem,
            string country,
            string city)
        {
            this.From = from.StartDay();
            this.To = to.EndDay();
            this.ApplicationId = applicationId;
            this.ScreenSize = screenSize;
            this.Path = path;
            this.SecondPath = secondPath;
            this.Language = language;
            this.OperatingSystem = operatingSystem;
            this.Country = country;
            this.City = city;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Queries.Analytics.QueryResults;

namespace EyeTracker.Common.Queries.Analytics
{
    public class UsageViewData : IQuery<UsageViewDataResult>
    {
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public int? PortfolioId { get; private set; }
        public int? ApplicationId { get; private set; }
        public DataGrouping DataGrouping { get; private set; }
        public int? ScreenHeight { get; private set; }
        public int? ScreenWidth { get; private set; }
        public string Path { get; private set; }
        public string Language { get; private set; }
        public string OperatingSystem { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }

        public UsageViewData(
            DateTime from, 
            DateTime to, 
            int? portfolioId, 
            int? applicationId, 
            int? screenHeight,
            int? screenWidth,
            string path,
            string language,
            string operatingSystem,
            string country,
            string city,
            DataGrouping dataGrouping)
        {
            this.From = from;
            this.To = to;
            this.PortfolioId = portfolioId;
            this.ApplicationId = applicationId;
            this.ScreenHeight = screenHeight;
            this.ScreenWidth = screenWidth;
            this.Path = path;
            this.Language = language;
            this.OperatingSystem = operatingSystem;
            this.Country = country;
            this.City = city;
            this.DataGrouping = dataGrouping;
        }
    }
}

using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace EyeTracker.Model.Pages.Analytics
{
    public class FilterParametersModel
    {
        public int PortfolioId { get; set; }

        public DateTime FromDate { get; set;  }

        public DateTime ToDate { get; set; }

        public int? ApplicationId { get; set; }

        public Size? ScreenSize { get; set; }

        public string Path { get; set; }

        public string Language { get; set; }

        public string OperationSystem { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
    }
}
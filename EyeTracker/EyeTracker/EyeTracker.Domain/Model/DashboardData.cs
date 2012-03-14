using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Common;

namespace EyeTracker.Domain.Model
{
    public class DashboardData
    {
        public Dictionary<DateTime, int> ViewsData { get; set; }

        public string Description { get; set; }

        public Dictionary<int, string> Applications { get; set; }

        public IEnumerable<PortfolioDetails> Portfolios { get; set; }
    }

    public class ApplicationDashboardData : DashboardData
    {
        public int PortfolioId { get; set; }

        public string PortfolioDescription { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EyeTracker.Model.Filter;

namespace EyeTracker.Model.Master
{
    public class AnalyticsModel
    {
        public int PortfolioId { get; set; }
        public FilterModel Filter { get; set; }
    }
}
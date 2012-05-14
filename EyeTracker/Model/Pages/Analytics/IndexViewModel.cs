using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Domain.Common;
using EyeTracker.Model.Filter;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;

namespace EyeTracker.Model.Pages.Analytics
{
    public class IndexViewModel : FilterModel
    {
        public IEnumerable<PortfolioResult> Portfolios1 { get; private set; }

        public int PortfoliosCount { get { return Portfolios1.Count(); } }

        public IndexViewModel(IEnumerable<PortfolioResult> portfolios)
        {
            this.Portfolios1 = portfolios;
        }
    }
}
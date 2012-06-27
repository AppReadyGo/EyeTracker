using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;
using EyeTracker.Common.QueryResults.Analytics.QueryResults;

namespace EyeTracker.Model.Pages.Analytics
{
    public class IndexViewModel : FilterModel
    {
        public IEnumerable<PortfolioResult> PortfoliosInfo { get; private set; }

        public int PortfoliosCount { get { return PortfoliosInfo.Count(); } }

        public IndexViewModel(IEnumerable<PortfolioResult> portfolios)
        {
            this.PortfoliosInfo = portfolios;
        }
    }
}
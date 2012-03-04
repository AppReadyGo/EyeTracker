using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Domain.Common;

namespace EyeTracker.Model.Pages.Analytics
{
    public class IndexViewModel
    {
        public IEnumerable<PortfolioDetails> Portfolios { get; private set; }

        public int PortfoliosCount { get { return Portfolios.Count(); } }

        public IndexViewModel(IEnumerable<PortfolioDetails> portfolios)
        {
            this.Portfolios = portfolios;
        }
    }
}
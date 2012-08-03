using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Common.QueryResults.Portfolio;

namespace EyeTracker.Model.Pages.Portfolio
{
    public class PortfolioIndexModel : PagingModel
    {
        public IEnumerable<PortfolioItemModel> Portfolios { get; set; }
    }

    public class PortfolioItemModel : PortfolioDataItemResult
    {
        public bool IsAlternative { get; set; }
    }
}
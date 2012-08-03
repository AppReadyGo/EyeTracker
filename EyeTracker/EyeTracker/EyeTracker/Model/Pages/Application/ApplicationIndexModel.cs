using System.Collections.Generic;
using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Model.Pages.Portfolio
{
    public class ApplicationIndexModel : PagingModel
    {
        public IEnumerable<ApplicationItemModel> Applications { get; set; }

        public string PortfolioDescription { get; set; }

        public int PortfolioId { get; set; }
    }

    public class ApplicationItemModel : ApplicationDataItemResult
    {
        public string Key { get; set; }

        public bool IsAlternative { get; set; }
    }
}
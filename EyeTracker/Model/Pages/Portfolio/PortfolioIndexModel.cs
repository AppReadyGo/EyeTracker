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

        public IEnumerable<TopApplicationsItemModel> TopApplications { get; set; }

        public IEnumerable<TopScreensItemModel> TopScreens { get; set; }
    }

    public class PortfolioItemModel : PortfolioDataItemResult
    {
        public bool IsAlternative { get; set; }
    }

    public class TopApplicationsItemModel
    {
        public int PortfolioId { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsAlternative { get; set; }
    }

    public class TopScreensItemModel
    {
        public int PortfolioId { get; set; }

        public int ApplicationId { get; set; }

        public string ScreenSize { get; set; }

        public string Path { get; set; }

        public bool IsAlternative { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Common.QueryResults.Portfolio;
using EyeTracker.Model.Master;
using System.Web.Mvc;

namespace EyeTracker.Model.Pages.Portfolio
{
    public class PortfolioIndexModelTmp : AfterLoginMasterModel, IPagingModel
    {
        public IEnumerable<ApplicationItemModel> Applications { get; set; }

        public IEnumerable<TopApplicationsItemModel> TopApplications { get; set; }

        public IEnumerable<TopScreensItemModel> TopScreens { get; set; }

        #region IPagingModel Members

        public bool IsOnePage { get; set; }

        public int? PreviousPage { get; set; }

        public int? NextPage { get; set; }

        public int Count { get; set; }

        public int TotalPages { get; set; }

        public int CurPage { get; set; }

        public string UrlPart { get; set; }

        public string SearchStr { get; set; }

        public string SearchStrUrlPart { get; set; }

        #endregion

        public PortfolioIndexModelTmp(Controller controller, MenuItem selectedItem)
            : base(controller, selectedItem)
        {
        }
    }

    public class PortfolioIndexModel : PagingModel
    {
        public IEnumerable<ApplicationItemModel> Applications { get; set; }

        public IEnumerable<TopApplicationsItemModel> TopApplications { get; set; }

        public IEnumerable<TopScreensItemModel> TopScreens { get; set; }
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Model.Pages.Application
{
    public class ScreensListModel : PagingModel
    {
        public int ApplicationId { get; set; }

        public string ApplicationDescription { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioDescription { get; set; }

        public IEnumerable<ScreenItemModel> Screens { get; set; }

        public string PathOrder { get; set; }

        public string WidthOrder { get; set; }

        public string HeightOrder { get; set; }
    }

    public class ScreenItemModel : ScreenDataItemResult
    {
        public bool IsAlternative { get; set; }

        public string FileExtention { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EyeTracker.Model.Filter;
using System.Web.Mvc;

namespace EyeTracker.Model.Master
{
    public class AnalyticsMasterModel : AfterLoginMasterModel
    {
        public MenuItem SelectedMenuItem { get; private set; }

        public string FilterUrlPart { get; protected set; }

        public int PortfolioId { get; protected set; }

        /// <summary>
        /// Disable menu when Filter does not have url
        /// </summary>
        public bool IsMenuDisabled { get { return string.IsNullOrEmpty(this.FilterUrlPart); } }

        public AnalyticsMasterModel(Controller controller, MenuItem selectedItem)
            : base(controller, AfterLoginMasterModel.MenuItem.Analytics)
        {
            this.SelectedMenuItem = selectedItem;
        }

        public string GetMenuItemClass(MenuItem item)
        {
            return item == this.SelectedMenuItem ? "active" : string.Empty;
        }

        public enum MenuItem
        {
            Portfolios,
            Dashboard,
            TouchMap,
            EyeTracker,
            ABCompare,
            Usage,
            Visitors,
            MapOverlay,
            TraficSources,
            ContentOverview
        }
    }
}
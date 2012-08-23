using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EyeTracker.Model.Filter;

namespace EyeTracker.Model.Master
{
    public class AnalyticsMasterModel
    {
        public MenuItem SelectedItem { get; private set; }
        public string FilterUrlPart { get; private set; }
        public int? PortfolioId { get; private set; }

        public bool IsMenuDisabled { get { return string.IsNullOrEmpty(this.FilterUrlPart); } }

        public AnalyticsMasterModel(MenuItem selectedItem, string filterUrlPart, int? portfolioId)
        {
            this.SelectedItem = selectedItem;
            this.FilterUrlPart = filterUrlPart;
            this.PortfolioId = portfolioId;
        }

        public string GetMenuItemClass(MenuItem item)
        {
            return item == this.SelectedItem ? "active" : string.Empty;
        }

        public enum MenuItem
        {
            Portfolios,
            Dashboard,
            FingerPrint,
            EyeTracker,
            Usage,
            Visitors,
            MapOverlay,
            TraficSources,
            ContentOverview
        }
    }
}
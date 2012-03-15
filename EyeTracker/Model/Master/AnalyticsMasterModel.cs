using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EyeTracker.Model.Filter;

namespace EyeTracker.Model.Master
{
    public class AnalyticsMasterModel
    {
        public FilterModel Filter { get; set; }
        public MenuItem SelectedItem { get; set; }
        public string FilterUrlPart { get; set; }

        public AnalyticsMasterModel(MenuItem selectedItem, string filterUrlPart)
        {
            this.SelectedItem = selectedItem;
            this.FilterUrlPart = filterUrlPart;
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
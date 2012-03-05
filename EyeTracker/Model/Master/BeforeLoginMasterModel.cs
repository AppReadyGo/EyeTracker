using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Master
{
    public class BeforeLoginMasterModel
    {
        public MenuItem SelectedItem { get; private set; }

        public BeforeLoginMasterModel()
        {
        }

        public BeforeLoginMasterModel(MenuItem selectedItem)
        {
            this.SelectedItem = selectedItem;
        }

        public string GetMenuItemClass(MenuItem item)
        {
            return item == this.SelectedItem ? "selected" : string.Empty;
        }

        public enum MenuItem
        {
            Home,
            Tutorials,
            Products,
            PlanAndPricing
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EyeTracker.Model.Filter;

namespace EyeTracker.Model.Pages.Admin
{
    public class AdminMasterModel
    {
        public MenuItem SelectedItem { get; set; }

        public AdminMasterModel(MenuItem selectedItem)
        {
            this.SelectedItem = selectedItem;
        }

        public string GetMenuItemClass(MenuItem item)
        {
            return item == this.SelectedItem ? "active" : string.Empty;
        }

        public enum MenuItem
        {
            Logs,
            Staff,
            Members
        }
    }
}
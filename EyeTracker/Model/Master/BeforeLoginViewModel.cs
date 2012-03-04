using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Master
{
    public class BeforeLoginViewModel
    {
        public SelectedMenuItem SelectedItem { get; private set; }
        public string HomeCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Home ? "class=\"selected\"" : string.Empty; } }
        public string TutorialsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Tutorials ? "class=\"selected\"" : string.Empty; } }
        public string ProductsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Products ? "class=\"selected\"" : string.Empty; } }
        public string PlanAndPricingCssClassAttribute { get { return SelectedItem == SelectedMenuItem.PlanAndPricing ? "class=\"selected\"" : string.Empty; } }

        public BeforeLoginViewModel()
        {
        }

        public BeforeLoginViewModel(SelectedMenuItem selectedItem)
        {
            this.SelectedItem = selectedItem;
        }

        public enum SelectedMenuItem
        {
            Home,
            Tutorials,
            Products,
            PlanAndPricing
        }
    }
}
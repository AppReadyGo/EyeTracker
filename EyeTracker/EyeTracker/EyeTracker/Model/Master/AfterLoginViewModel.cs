using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Master
{
    public class AfterLoginViewModel
    {
        public SelectedMenuItem SelectedItem { get; private set; }
        public string AnalyticsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Analytics ? "class=\"selected\"" : string.Empty; } }
        public string MyAccountCssClassAttribute { get { return SelectedItem == SelectedMenuItem.MyAccount ? "class=\"selected\"" : string.Empty; } }
        public string TutorialsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Tutorials ? "class=\"selected\"" : string.Empty; } }
        public string ProductsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Products ? "class=\"selected\"" : string.Empty; } }
        public string PlanAndPricingCssClassAttribute { get { return SelectedItem == SelectedMenuItem.PlanAndPricing ? "class=\"selected\"" : string.Empty; } }
        public string ContactsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Contacts ? "class=\"selected\"" : string.Empty; } }

        public AfterLoginViewModel()
        {
        }

        public AfterLoginViewModel(SelectedMenuItem selectedItem)
        {
            this.SelectedItem = selectedItem;
        }

        public enum SelectedMenuItem
        {
            Analytics,
            MyAccount,
            Tutorials,
            Products,
            PlanAndPricing,
            Contacts
        }
    }
}
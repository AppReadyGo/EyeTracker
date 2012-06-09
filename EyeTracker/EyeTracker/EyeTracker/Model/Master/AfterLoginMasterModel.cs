using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Model.Master
{
    public class AfterLoginMasterModel
    {
        public SelectedMenuItem SelectedItem { get; private set; }
        public string AnalyticsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Analytics ? "class=\"current\"" : string.Empty; } }
        public string MyAccountCssClassAttribute { get { return SelectedItem == SelectedMenuItem.MyAccount ? "class=\"current\"" : string.Empty; } }
        public string TutorialsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Tutorials ? "class=\"current\"" : string.Empty; } }
        public string ProductsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Products ? "class=\"current\"" : string.Empty; } }
        public string PlanAndPricingCssClassAttribute { get { return SelectedItem == SelectedMenuItem.PlanAndPricing ? "class=\"current\"" : string.Empty; } }
        public string ContactsCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Contacts ? "class=\"current\"" : string.Empty; } }
        public string AdministratorCssClassAttribute { get { return SelectedItem == SelectedMenuItem.Administrator ? "class=\"current\"" : string.Empty; } }
        public string UserName { get; set; }

        public AfterLoginMasterModel()
        {
        }

        public AfterLoginMasterModel(SelectedMenuItem selectedItem)
        {
            this.SelectedItem = selectedItem;
            this.UserName = "Yuri Panshin";
        }

        public enum SelectedMenuItem
        {
            Analytics,
            MyAccount,
            Tutorials,
            Products,
            PlanAndPricing,
            Contacts,
            Administrator
        }

        public bool IsAdmin { get; set; }
    }
}
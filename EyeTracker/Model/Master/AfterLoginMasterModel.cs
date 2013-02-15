using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Core;
using System.Web.Mvc;
using EyeTracker.Common;

namespace EyeTracker.Model.Master
{
    public class AfterLoginMasterModel : MainMasterModel
    {
        public MenuItem SelectedItem { get; private set; }

        public string AnalyticsCssClassAttribute { get { return SelectedItem == MenuItem.Analytics ? "class=\"current\"" : string.Empty; } }

        public string MyAccountCssClassAttribute { get { return SelectedItem == MenuItem.MyAccount ? "class=\"current\"" : string.Empty; } }

        public string TutorialsCssClassAttribute { get { return SelectedItem == MenuItem.Tutorials ? "class=\"current\"" : string.Empty; } }

        public string ProductsCssClassAttribute { get { return SelectedItem == MenuItem.Products ? "class=\"current\"" : string.Empty; } }

        public string PlanAndPricingCssClassAttribute { get { return SelectedItem == MenuItem.PlanAndPricing ? "class=\"current\"" : string.Empty; } }

        public string ContactsCssClassAttribute { get { return SelectedItem == MenuItem.Contacts ? "class=\"current\"" : string.Empty; } }

        public string AdministratorCssClassAttribute { get { return SelectedItem == MenuItem.Administrator ? "class=\"current\"" : string.Empty; } }

        public string CurrentUserDisplayName { get; private set; }

        public AfterLoginMasterModel(Controller controller ,MenuItem selectedItem)
        {
            this.SelectedItem = selectedItem;
            this.CurrentUserDisplayName = ObjectContainer.Instance.CurrentUserDetails.DisplayName;
            this.IsAdmin = controller.User.IsInRole(StaffRole.Administrator.ToString());
        }

        public enum MenuItem
        {
            Analytics,
            MyAccount,
            Tutorials,
            Products,
            PlanAndPricing,
            Contacts,
            Administrator,
            None
        }

        public bool IsAdmin { get; set; }
    }
}
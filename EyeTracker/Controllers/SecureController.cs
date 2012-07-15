using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Controllers.Master;
using EyeTracker.Model.Master;
using EyeTracker.Models.Account;
using EyeTracker.Core;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common;
using EyeTracker.Common.Commands.Users;
using EyeTracker.Model.Pages.Home;
using EyeTracker.Common.Queries.Content;

namespace EyeTracker.Controllers
{
    [Authorize]
    public class SecureController : AfterLoginController
    {
        public override AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.MenuItem.None; }
        }

        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(ObjectContainer.Instance.CurrentUserDetails.Email));
                if (securedDetails.Password == Encryption.SaltedHash(model.OldPassword, securedDetails.PasswordSalt))
                {
                    var result = ObjectContainer.Instance.Dispatch(new ResetPasswordCommand(securedDetails.Email, model.NewPassword));
                    if (result.Validation.Any())
                    {
                        //Redirect to error page
                    }
                    //return Redirect("~/p/secure/password-changed-successful");
                    return Redirect("~/");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect.");
                }
            }

            return View(model, BeforeLoginMasterModel.MenuItem.None);
        }

        /// <summary>
        /// After login page content
        /// </summary>
        /// <param name="urlPart1"></param>
        /// <param name="urlPart2"></param>
        /// <param name="urlPart3"></param>
        /// <returns></returns>
        public ActionResult PageContent(string urlPart1, string urlPart2, string urlPart3)
        {
            string path = urlPart1;
            if (!string.IsNullOrEmpty(urlPart2))
            {
                path += "/" + urlPart2;
            }
            if (!string.IsNullOrEmpty(urlPart3))
            {
                path += "/" + urlPart3;
            }

            var page = ObjectContainer.Instance.RunQuery(new GetPageQuery(path.ToLower()));
            if (page == null)
            {
                return null;// View("404", new PricingModel { }, BeforeLoginMasterModel.MenuItem.None);
            }
            else
            {
                var selectedItem = BeforeLoginMasterModel.MenuItem.None;
                if (!Enum.TryParse<BeforeLoginMasterModel.MenuItem>(urlPart1, true, out selectedItem))
                {
                    selectedItem = BeforeLoginMasterModel.MenuItem.None;
                }
                return View(new ContentModel { Title = page.Title, Content = page.Content }, selectedItem);
            }
        }
    }
}

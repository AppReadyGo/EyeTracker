using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using EyeTracker.Models;
using EyeTracker.Core.Services;
using EyeTracker.DAL.Domain;
using EyeTracker.Models.Account;
using EyeTracker.Model.Master;
using EyeTracker.Model;
using EyeTracker.Core;
using EyeTracker.Common.Commands.Users;
using EyeTracker.Common.Mails;
using EyeTracker.Model.Pages.Account;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common.Commands;
using EyeTracker.Common;

namespace EyeTracker.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return this.View(new LogOnModel(), BeforeLoginMasterModel.MenuItem.None);
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(model.UserName));
                if (securedDetails == null || securedDetails.Password != Encryption.SaltedHash(model.Password, securedDetails.PasswordSalt))
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
                else if(!securedDetails.Activated)
                {
                    ModelState.AddModelError("", "You account is not activated, please use the link from activation email to activate your account.");
                }
                else if (!Membership.Provider.ValidateUser(model.UserName, model.Password))
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
                else
                {
                    // TODO: add terms and conditions
                    FormsAuthentication.SetAuthCookie(securedDetails.Id.ToString(), model.RememberMe);
                    //if (securedDetails.Roles != null)
                    //{
                    //    foreach(var r in securedDetails.Roles.Select(r => r.ToString()))
                    //    {
                    //        if(!Roles.IsUserInRole(securedDetails.Id.ToString(), r))
                    //        {
                    //            Roles.AddUserToRole(securedDetails.Id.ToString(), r);
                    //        }
                    //    }
                        
                    //}
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return this.View(model, BeforeLoginMasterModel.MenuItem.None);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            ObjectContainer.Instance.ClearCurrentUserDetails();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return this.View(new RegisterModel(), BeforeLoginMasterModel.MenuItem.None);
        }

        private ActionResult View<TViewModel>(TViewModel viewModel, BeforeLoginMasterModel.MenuItem selectedItem)
        {
            var model = new ViewModelWrapper<BeforeLoginMasterModel, TViewModel>(new BeforeLoginMasterModel(selectedItem), viewModel);

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = ObjectContainer.Instance.Dispatch(new CreateMemberCommand(model.Email, model.Password));

                if (!result.Validation.Any())
                {
                    //TODO: send welcome email
                    new MailGenerator(this.ControllerContext).Send(new PromotionEmail("thank-you", model.Email));
                    return Redirect("~/s/thank-you");
                    ////Waiting for activation
                    //new MailGenerator(this.ControllerContext).Send(new ActivationEmail(model.Email));
                    //return Redirect("/activation-email-sent");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(result.Validation));
                }
            }

            return this.View(model, BeforeLoginMasterModel.MenuItem.None);
        }

        private string ErrorCodeToString(IEnumerable<ValidationResult> validations)
        {
            if(validations.Any(v => v.ErrorCode == ErrorCode.EmailExists))
            {
                return "User with the email already exists, please check your email.";
            }
            return "System error, please contact to administrator.";
        }

        public ActionResult Activate(string key)
        {
            var splitedKey = key.DecryptLow().Split(',');
            if (DateTime.Now > DateTime.Parse(splitedKey[0]))
            {
                throw new Exception("Activation link expired.");
            }
            var result = ObjectContainer.Instance.Dispatch(new ActivateUserCommand(splitedKey[1]));
            if (result.Validation.Any())
            {
                throw new Exception("User does not found.");
            }
            return Redirect("~/s/account-activated"); ;
        }

        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordModel(), BeforeLoginMasterModel.MenuItem.None);
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var userDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(model.Email));
                if (userDetails != null)
                {
                    new MailGenerator(this.ControllerContext).Send(new ForgotPasswordMail(model.Email));
                    return Redirect("~/s/forgot-password-email-sent"); // Redirect to content page
                }
                else
                {
                    ModelState.AddModelError("", "The user does not exits in the system.");
                }
            }

            return View(model, BeforeLoginMasterModel.MenuItem.None);
        }

        public ActionResult ResetPassword(string key)
        {
            var splitedKey = key.DecryptLow().Split(',');
            if (DateTime.Now > DateTime.Parse(splitedKey[0]))
            {
                throw new Exception("Reset password link expired.");
            }
            var userDetails = ObjectContainer.Instance.RunQuery(new GetUserDetailsByEmailQuery(splitedKey[1]));
            if (userDetails == null)
            {
                throw new Exception("User does not found.");
            }
            return View(new ResetPasswordModel(), BeforeLoginMasterModel.MenuItem.None);
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model, string key)
        {
            if (ModelState.IsValid)
            {
                var splitedKey = key.DecryptLow().Split(',');
                var email = splitedKey[1];
                if (DateTime.Now > DateTime.Parse(splitedKey[0]))
                {
                    throw new Exception("Reset password link expired.");
                }
                var result = ObjectContainer.Instance.Dispatch(new ResetPasswordCommand(email, model.NewPassword));
                if (result.Validation.Any())
                {
                    ModelState.AddModelError("", "Wrong password.");
                }
                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Index", "Home");
            }

            return View(model, BeforeLoginMasterModel.MenuItem.None);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordModel(), BeforeLoginMasterModel.MenuItem.None);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(User.Identity.Name));
                if (securedDetails.Password == Encryption.SaltedHash(model.OldPassword, securedDetails.PasswordSalt))
                {
                    var result = ObjectContainer.Instance.Dispatch(new ResetPasswordCommand(securedDetails.Email, model.NewPassword));
                    if (result.Validation.Any())
                    {
                        //Redirect to error page
                    }
                    return Redirect("~/s/password-changed-successful");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect.");
                }
            }

            return View(model, BeforeLoginMasterModel.MenuItem.None);
        }

        public ActionResult Unsubscribe(string email)
        {
            return View(new UnsubscribeModel { Email = email }, BeforeLoginMasterModel.MenuItem.None);
        }

        [HttpPost]
        public ActionResult Unsubscribe(UnsubscribeModel model)
        {
            if (ModelState.IsValid)
            {
                var result = ObjectContainer.Instance.Dispatch(new UnsubscribeCommand(model.Email));
                if (result.Validation.Any())
                {
                    ModelState.AddModelError("", "Wrong email.");
                }
                return Redirect("~/s/unsubscrubed-successful");
            }
            return View(model, BeforeLoginMasterModel.MenuItem.None);
        }


        public ActionResult Details()
        {
            return View();
        }
    }
}

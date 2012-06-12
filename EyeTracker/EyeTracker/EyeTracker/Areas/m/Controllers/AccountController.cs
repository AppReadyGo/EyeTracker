﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Core;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Model.Master;
using EyeTracker.Model.Pages.Home;
using EyeTracker.Models.Account;
using EyeTracker.Common.Commands.Users;
using EyeTracker.Common.Mails;
using EyeTracker.Common;
using EyeTracker.Common.Commands;

namespace EyeTracker.Areas.m.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return this.View(new RegisterModel());
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
                    return Redirect("/m/s/thank-you");
                    ////Waiting for activation
                    //new MailGenerator(this.ControllerContext).Send(new ActivationEmail(model.Email));
                    //return Redirect("/activation-email-sent");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(result.Validation));
                }
            }

            return this.View(model);
        }

        private string ErrorCodeToString(IEnumerable<ValidationResult> validations)
        {
            if (validations.Any(v => v.ErrorCode == ErrorCode.EmailExists))
            {
                return "User with the email already exists, please check your email.";
            }
            return "System error, please contact to administrator.";
        }
    }
}

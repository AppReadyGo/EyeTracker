using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Core.Services;
using EyeTracker.Model;
using EyeTracker.Domain.Model;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        IAdminService adminService;
        public AdminController()
            : this(new AdminService())
        {
        }

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Elmah(string query)
        {
            return new ElmahResult(query);
        }

        public ActionResult Membership()
        {
            var membership = adminService.GetMembership();
            ViewBag.Applications = membership.Applications;
            ViewBag.Roles = membership.Roles;
            ViewBag.Users = membership.Users;
            return View();
        }

        public ActionResult UserEdit(Guid id)
        {
            SystemUser curUser = adminService.Get<SystemUser>(id);
            var userModel = new UserModel()
            {
                Id = curUser.Id,
                Name = curUser.Name,
                Email = curUser.Email,
                Roles = curUser.Roles.Select(curItem => curItem.Id).ToList()
            };
            IList<SystemRole> roles = adminService.GetAll<SystemRole>();
            ViewBag.Roles = roles;
            return View(userModel);
        }

        [HttpPost]
        public ActionResult UserEdit(UserModel userModel)
        {
            IList<SystemRole> roles = adminService.GetAll<SystemRole>();
            ViewBag.Roles = roles;
            if (ModelState.IsValid)
            {
                var user = adminService.Get<SystemUser>(userModel.Id);
                user.Name = userModel.Name;
                user.Email = userModel.Email;
                if (userModel.Roles != null)
                {
                    user.Roles = roles.Where(curItem => userModel.Roles.Contains(curItem.Id)).ToList();
                }
                else
                {
                    user.Roles = null;
                }
                adminService.Edit<SystemUser>(user);
                return RedirectToAction("Membership");
            }
            return View(userModel);
        }

    }

    class ElmahResult : ActionResult
    {
        private string _resouceType;

        public ElmahResult(string resouceType)
        {
            _resouceType = resouceType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var factory = new Elmah.ErrorLogPageFactory();

            if (!string.IsNullOrEmpty(_resouceType))
            {
                var pathInfo = "/" + _resouceType;
                context.HttpContext.RewritePath(FilePath(context), pathInfo, context.HttpContext.Request.QueryString.ToString());
            }

            var currentApplication = (HttpApplication)context.HttpContext.GetService(typeof(HttpApplication));
            var currentContext = currentApplication.Context;

            var httpHandler = factory.GetHandler(currentContext, null, null, null);
            if (httpHandler is IHttpAsyncHandler)
            {
                var asyncHttpHandler = (IHttpAsyncHandler)httpHandler;
                asyncHttpHandler.BeginProcessRequest(currentContext, (r) => { }, null);
            }
            else
            {
                httpHandler.ProcessRequest(currentContext);
            }
        }

        private string FilePath(ControllerContext context)
        {
            return _resouceType != "stylesheet" ? context.HttpContext.Request.Path.Replace(String.Format("/{0}", _resouceType), string.Empty) : context.HttpContext.Request.Path;
        }
    }
}

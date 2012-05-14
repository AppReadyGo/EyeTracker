using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Core.Services;
using EyeTracker.Model;
using EyeTracker.Domain.Model;
using EyeTracker.Domain.Model.BackOffice;
using EyeTracker.Controllers.Master;
using EyeTracker.Model.Pages.Admin;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Core;

namespace EyeTracker.Controllers
{
   // [Authorize(Roles = "Administrators")]
    public class AdminController : AfterLoginController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        
        public override Model.Master.AfterLoginMasterModel.SelectedMenuItem SelectedMenuItem
        {
            get { return Model.Master.AfterLoginMasterModel.SelectedMenuItem.Analytics; }
        }

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
            return View(new AdminModel());
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
            return View(new AdminModel());
        }

        public ActionResult UserEdit(Guid id)
        {
            SystemUser curUser = adminService.Get<SystemUser>(id);
            var userModel = new UserModel()
            {
                Id = curUser.Id,
                Name = curUser.Name,
                Email = curUser.Membership.Email,
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
                user.Membership.Email = userModel.Email;
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

        public ActionResult Logs()
        {
            log.WriteInformation("--> Logs");

            var result = ObjectContainer.Instance.RunQuery(new LogDataQuery());

            ViewBag.Logs = string.Join("\n", result.Log.Select(a => a.ToString()));
            var categories = result.Categories.OrderBy(c => c.Value).Select(c => new SelectListItem { Text = c.Value, Value = c.Key.ToString() }).ToList();
            categories.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
            ViewBag.Categories = categories;
            var severities = result.Severities.OrderBy(s => s).Select(s => new SelectListItem { Text = s, Value = s }).ToList();
            severities.Insert(0, new SelectListItem { Text = "All", Value = string.Empty, Selected = true });
            ViewBag.Severities = severities;
            log.WriteInformation("Logs-->");
            return View();
        }

        public ActionResult ClearLogs()
        {
            var result = ObjectContainer.Instance.Dispatch(new ClearLogCommand());
            return RedirectToAction("Logs");
        }

        [HttpPost]
        public ActionResult Logs(LogsModel model)
        {
            int? categoryId = model.CategoryId == 0 ? null : (int?)model.CategoryId;
            model.Severity = model.Severity == "0" ? null : model.Severity;
            var result = ObjectContainer.Instance.RunQuery(new LogDataQuery(model.SearchStr, model.FromDate, model.ToDate, categoryId, model.Severity, model.ProcessId, model.ThreadId));

            ViewBag.Logs = string.Join("\n", result.Log.Select(a => a.ToString()));
            var categories = result.Categories.OrderBy(c => c.Value).Select(c => new SelectListItem { Text = c.Value, Value = c.Key.ToString() }).ToList();
            categories.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
            ViewBag.Categories = categories;
            var severities = result.Severities.OrderBy(s => s).Select(s => new SelectListItem { Text = s, Value = s }).ToList();
            severities.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
            ViewBag.Severities = severities;

            return View(model);
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

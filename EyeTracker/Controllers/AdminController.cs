using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Common.Commands.Admin;
using EyeTracker.Common.Logger;
using EyeTracker.Common.Mails;
using EyeTracker.Common.Queries.Admin;
using EyeTracker.Controllers.Master;
using EyeTracker.Core;
using EyeTracker.Core.Services;
using EyeTracker.Domain.Model.BackOffice;
using EyeTracker.Domain.Model.Users;
using EyeTracker.Model;
using EyeTracker.Model.Pages.Admin;
using EyeTracker.Model.Master;
using EyeTracker.Common;
using System.Web.Security;
using EyeTracker.Common.Commands.Users;

namespace EyeTracker.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : AdminMasterController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        
        public ActionResult Index()
        {
            return RedirectToAction("Logs");
        }

        public ActionResult Membership()
        {
            //var membership = adminService.GetMembership();
            //ViewBag.Applications = membership.Applications;
            //ViewBag.Roles = membership.Roles;
            //ViewBag.Users = membership.Users;
            return View(new AdminModel());
        }

        public ActionResult UserEdit(Guid id)
        {
            //SystemUser curUser = adminService.Get<SystemUser>(id);
            //var userModel = new UserModel()
            //{
            //    Id = curUser.Id,
            //    Name = curUser.Name,
            //    Email = curUser.Membership.Email,
            //    Roles = curUser.Roles.Select(curItem => curItem.Id).ToList()
            //};
            //IList<SystemRole> roles = adminService.GetAll<SystemRole>();
            //ViewBag.Roles = roles;
            return null;// View(userModel);
        }

        [HttpPost]
        public ActionResult UserEdit(UserModel userModel)
        {
            //IList<SystemRole> roles = adminService.GetAll<SystemRole>();
            //ViewBag.Roles = roles;
            //if (ModelState.IsValid)
            //{
            //    var user = adminService.Get<SystemUser>(userModel.Id);
            //    user.Name = userModel.Name;
            //    user.Membership.Email = userModel.Email;
            //    if (userModel.Roles != null)
            //    {
            //        user.Roles = roles.Where(curItem => userModel.Roles.Contains(curItem.Id)).ToList();
            //    }
            //    else
            //    {
            //        user.Roles = null;
            //    }
            //    adminService.Edit<SystemUser>(user);
            //    return RedirectToAction("Membership");
            //}
            return View(userModel);
        }

        public ActionResult Staff(string srch = "", int scol = 1, int cp = 1, string orderby = "", string order = "")
        {
            bool asc = string.IsNullOrEmpty(orderby) ? true : order.Equals("asc", StringComparison.OrdinalIgnoreCase);
            var orderBy = string.IsNullOrEmpty(orderby) ? GetAllStaffQuery.OrderByColumn.Email : (GetAllStaffQuery.OrderByColumn)Enum.Parse(typeof(GetAllStaffQuery.OrderByColumn), orderby, true);
            var data = ObjectContainer.Instance.RunQuery(new GetAllStaffQuery(srch, orderBy, asc, cp, 15));

            var searchStrUrlPart = string.IsNullOrEmpty(srch) ? string.Empty : string.Concat("&srch=", HttpUtility.UrlEncode(srch));
            var model = new StaffPagingModel 
            { 
                IsOnePage = data.TotalPages == 1,
                Count = data.Count,
                PreviousPage = data.CurPage == 1 ? null : (int?)(data.CurPage - 1),
                NextPage = data.CurPage == data.TotalPages ? null : (int?)(data.CurPage + 1),
                TotalPages = data.TotalPages,
                CurPage = data.CurPage,
                UrlPart = string.Concat(searchStrUrlPart, string.IsNullOrEmpty(orderby) ? string.Empty : string.Concat("&orderby=", orderby), string.IsNullOrEmpty(order) ? string.Empty : string.Concat("&order=", order)),
                SearchStrUrlPart = searchStrUrlPart,
                SearchStr = srch,
                EmailOrder = orderBy == GetAllStaffQuery.OrderByColumn.Email && asc ? "desc" : "asc",
                NameOrder = orderBy == GetAllStaffQuery.OrderByColumn.Name && asc ? "desc" : "asc",
                Users = data.Users.Select((u, i) => new StaffDetailsModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = string.IsNullOrEmpty(u.FirstName + u.LastName) ? string.Empty : string.Concat(u.FirstName, " ", u.LastName),
                    Roles = string.Join(", ", u.Roles.Select(r => r.ToString())),
                    Activated = u.Activated,
                    Index = i,
                    IsAlternative = i % 2 != 0,
                    LastAccess = u.LastAccessDate.HasValue ? u.LastAccessDate.Value.ToString("dd MMM yyyy") : string.Empty
                }).ToArray()
            };
            return View(model, AdminMasterModel.MenuItem.Staff);
        }

        public ActionResult Members(string srch = "", int scol = 1, int cp = 1, string orderby = "", string order = "")
        {
            var orderBy = string.IsNullOrEmpty(orderby) ? GetAllMembersQuery.OrderByColumn.CreateDate : (GetAllMembersQuery.OrderByColumn)Enum.Parse(typeof(GetAllMembersQuery.OrderByColumn), orderby, true);
            bool asc = string.IsNullOrEmpty(orderby) ? ((orderBy == GetAllMembersQuery.OrderByColumn.CreateDate) ? false : true) : order.Equals("asc", StringComparison.OrdinalIgnoreCase);
            var data = ObjectContainer.Instance.RunQuery(new GetAllMembersQuery(srch, orderBy, asc, cp, 15));

            var searchStrUrlPart = string.IsNullOrEmpty(srch) ? string.Empty : string.Concat("&srch=", HttpUtility.UrlEncode(srch));
            var model = new MembersPagingModel
            {
                IsOnePage = data.TotalPages == 1,
                Count = data.Count,
                PreviousPage = data.CurPage == 1 ? null : (int?)(data.CurPage - 1),
                NextPage = data.CurPage == data.TotalPages ? null : (int?)(data.CurPage + 1),
                TotalPages = data.TotalPages,
                CurPage = data.CurPage,
                UrlPart = string.Concat(searchStrUrlPart, string.IsNullOrEmpty(orderby) ? string.Empty : string.Concat("&orderby=", orderby), string.IsNullOrEmpty(order) ? string.Empty : string.Concat("&order=", order)),
                SearchStrUrlPart = searchStrUrlPart,
                SearchStr = srch,
                EmailOrder = orderBy == GetAllMembersQuery.OrderByColumn.Email && asc ? "desc" : "asc",
                NameOrder = orderBy == GetAllMembersQuery.OrderByColumn.Name && asc ? "desc" : "asc",
                CreateDateOrder = orderBy == GetAllMembersQuery.OrderByColumn.CreateDate && asc ? "desc" : "asc",
                Users = data.Users.Select((u, i) => new MemberDetailsModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = string.IsNullOrEmpty(u.FirstName + u.LastName) ? string.Empty : string.Concat(u.FirstName, " ", u.LastName),
                    Activated = u.Activated,
                    SpecialAccess = u.SpecialAccess,
                    Index = i,
                    IsAlternative = i % 2 != 0,
                    LastAccess = u.LastAccessDate.HasValue ? u.LastAccessDate.Value.ToString("dd MMM yyyy") : string.Empty,
                    Registred = u.CreateDate.ToString("dd MMM yyyy")
                }).ToArray()
            };
            return View(model, AdminMasterModel.MenuItem.Members);
        }

        public ActionResult DeleteMember(int id)
        {
            var result = ObjectContainer.Instance.Dispatch(new RemoveUserCommand(id));
            return RedirectToAction("Members");
        }

        public ActionResult Activate(string email)
        {
            var result = ObjectContainer.Instance.Dispatch(new ActivateUserCommand(email));
            return RedirectToAction("Members");
        }

        public ActionResult Deactivate(int id)
        {
            var result = ObjectContainer.Instance.Dispatch(new DeactivateUserCommand(id));
            return RedirectToAction("Members");
        }

        public ActionResult ResendEmail(string email)
        {
            //resend welcome email
            //todo: use this:
            //var result = ObjectContainer.Instance.Dispatch(new ResendEmailCommand(id));
            try
            {
                new MailGenerator(this.ControllerContext).Send(new ActivationEmail(email));
            }
            finally
            {
                
            }
            return RedirectToAction("Members");
        }

        public ActionResult Logs()
        {
            var result = ObjectContainer.Instance.RunQuery(new LogDataQuery());

            ViewBag.Logs = string.Join("\n", result.Log.Select(a => a.ToString()));
            var categories = result.Categories.OrderBy(c => c.Value).Select(c => new SelectListItem { Text = c.Value, Value = c.Key.ToString() }).ToList();
            categories.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
            ViewBag.Categories = categories;
            var severities = result.Severities.OrderBy(s => s).Select(s => new SelectListItem { Text = s, Value = s }).ToList();
            severities.Insert(0, new SelectListItem { Text = "All", Value = string.Empty, Selected = true });
            ViewBag.Severities = severities;

            return View(new LogsModel(), AdminMasterModel.MenuItem.Logs);
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

            return View(model, AdminMasterModel.MenuItem.Logs);
        }

        /*
                 
        public ActionResult Elmah(string query)
        {
            return new ElmahResult(query);
        }

         */
    }


/*
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
 */
}

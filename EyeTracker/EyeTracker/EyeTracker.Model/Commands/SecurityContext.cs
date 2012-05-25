using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;

namespace EyeTracker.Common.Commands
{
    public interface ISecurityContext
    {
        CurrentUserDetails CurrentUser { get; }
    }

    public class CurrentUserDetails
    {
        public int Id { get; private set; }

        public CurrentUserDetails(int id)
        {
            this.Id = id;
        }
    }

    public class SecurityContext : ISecurityContext
    {
        public CurrentUserDetails CurrentUser { get; private set; }

        public SecurityContext()
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                this.CurrentUser = new CurrentUserDetails(int.Parse(HttpContext.Current.User.Identity.Name));
            }
        }
    }
}

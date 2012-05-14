using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace EyeTracker.Common.Commands
{
    public interface ISecurityContext
    {
        CurrentUserDetails CurrentUser { get; }
    }

    public class CurrentUserDetails
    {
        public Guid Id { get; private set; }

        public CurrentUserDetails(Guid id)
        {
            this.Id = id;
        }
    }

    public class SecurityContext : ISecurityContext
    {
        public CurrentUserDetails CurrentUser { get; private set; }

        public SecurityContext()
        {
            MembershipUser user = Membership.GetUser();
            if (user != null)
            {
                this.CurrentUser = new CurrentUserDetails(new Guid(user.ProviderUserKey.ToString()));
            }
        }
    }
}

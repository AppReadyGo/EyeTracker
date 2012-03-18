using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain;
using EyeTracker.Core.Services;
using System.Web.Security;

namespace EyeTracker.Core
{
    public class SecurityContext : ISecurityContext
    {
        private IMembershipService membershipService;

        public Guid UserId
        {
            get
            {
                return membershipService.GetCurrentUserId().Value;
            }
        }

        public SecurityContext(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }
    }
}

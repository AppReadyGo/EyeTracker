using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Models;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EyeTracker.Common;
using EyeTracker.Core.Services;
using EyeTracker.Core;
using EyeTracker.DAL.Domain;

namespace EyeTracker.Tests.FakeData
{
    public interface IFakeMembershipService : IMembershipService
    {
        void DeleteUsers();
        string LogIn(string userId);
        void LogOut();
        List<string> GetUserIds();
    }

    public class FakeMembershipService : IFakeMembershipService
    {
        private MembershipUser curUser = null;
        private ActivityTracking tracking;

        public FakeMembershipService(ActivityTracking tracking)
        {
            this.tracking = tracking;
        }
        
        public int MinPasswordLength
        {
            get { return 10; }
        }

        public bool ValidateUser(string userName, string password)
        {
            return (userName == "someUser" && password == "goodPassword");
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (userName == "duplicateUser")
            {
                return MembershipCreateStatus.DuplicateUserName;
            }

            // verify that values are what we expected
            Assert.AreEqual("goodPassword", password);
            Assert.AreEqual("goodEmail", email);

            return MembershipCreateStatus.Success;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return (userName == "someUser" && oldPassword == "goodOldPassword" && newPassword == "goodNewPassword");
        }


        public OperationResult<MembershipUser> GetCurrentUser()
        {
            if (curUser == null)
            {
                return new OperationResult<MembershipUser>(ErrorNumber.AccessDenied);
            }
            else
            {
                return new OperationResult<MembershipUser>(curUser);
            }
        }

        public string LogIn(string userId)
        {
            curUser = new MembershipUser(Membership.Provider.Name, "some user", userId, "someemail@email.com", null, null, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);

            tracking.Write(userId, UserActivityType.LogIn, null, null);

            return userId;
        }

        public void LogOut()
        {
            if (curUser != null)
            {
                tracking.Write(curUser.ProviderUserKey.ToString(), UserActivityType.LogOut, null, null);
            }
            curUser = null;
        }

        public void DeleteUsers()
        {
        }


        public List<string> GetUserIds()
        {
            return new List<string>();
        }


        public OperationResult<Guid> GetCurrentUserId()
        {
            throw new NotImplementedException();
        }


        public void DeleteUser(string userName)
        {
            throw new NotImplementedException();
        }
    }
}

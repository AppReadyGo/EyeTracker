using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using EyeTracker.Core;
using EyeTracker.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EyeTracker.DAL.Models;

namespace EyeTracker.Tests.FakeData
{
    class RealMembershipService : IFakeMembershipService
    {
        private MembershipUser curUser = null;
        private ActivityTracking tracking;
        private readonly MembershipProvider _provider;
        private Dictionary<string, string> usersList = new Dictionary<string, string>();

        public RealMembershipService(ActivityTracking tracking)
        {
            _provider = Membership.Provider;
            this.tracking = tracking;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
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
            if (usersList.ContainsKey(userId))
            {
                curUser = Membership.GetUser(usersList[userId]);
            }
            else
            {
                string userName = Utilites.RandomString(10);
                MembershipCreateStatus status;
                _provider.CreateUser(userName, Utilites.RandomString(10), Utilites.RandomString(4) + "@email.com", null, null, true, null, out status);
                curUser = Membership.GetUser(userName);
                userId = curUser.ProviderUserKey.ToString();
                usersList.Add(userId, userName);
            }

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
            foreach (var curUser in usersList)
            {
                _provider.DeleteUser(curUser.Value, true);
            }
        }

        public List<string> GetUserIds()
        {
            return usersList.Keys.ToList();
        }
    }
}

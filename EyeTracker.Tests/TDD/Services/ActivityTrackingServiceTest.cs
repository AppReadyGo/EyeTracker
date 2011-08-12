using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EyeTracker.Tests.FakeData;
using EyeTracker.Core.Services;
using EyeTracker.DAL.Domain;
using EyeTracker.Common;

namespace EyeTracker.Tests.TDD.Services
{
    [TestClass]
    public class ActivityTrackingServiceTest
    {
        private static IFakeMembershipService membershipService = null;

        private IFakeDataBase fakeDataBase = null;
        private IActivityTrackingService activityService = null;

        protected static string firstUserId, secondUserId;

        #region test routines
        [ClassInitialize()]
        public static void ClassInitialize(TestContext context)
        {
            membershipService = (IFakeMembershipService)Utilites.Container.Resolve<IMembershipService>();

            firstUserId = membershipService.LogIn(Utilites.RandomString(10));
            secondUserId = membershipService.LogIn(Utilites.RandomString(10));
            membershipService.LogOut();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            fakeDataBase = Utilites.Container.Resolve<IFakeDataBase>();
            activityService = Utilites.Container.Resolve<IActivityTrackingService>();

        }

        [TestCleanup()]
        public void TestCleanup()
        {
            membershipService.LogOut();
            var userIds = membershipService.GetUserIds();
            fakeDataBase.Clear(userIds);
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            membershipService.DeleteUsers();
        }

        #endregion test routines

        [TestMethod]
        public void WriteActivity()
        {
            //Check permissions
            var writeRes = activityService.Write(UserActivityType.LogIn, Utilites.RandomString(10));
            Assert.IsTrue(writeRes.HasError);
            Assert.AreEqual(ErrorNumber.AccessDenied, writeRes.Error);

            membershipService.LogIn(firstUserId);

            //Check description for max chars
            writeRes = activityService.Write(UserActivityType.LogIn, Utilites.RandomString(1001), null);
            Assert.IsTrue(writeRes.HasError);
            Assert.AreEqual(ErrorNumber.WrongDescription, writeRes.Error);

            //General write
            writeRes = activityService.Write(UserActivityType.LogIn, Utilites.RandomString(10), null);
            Assert.IsFalse(writeRes.HasError);
            var userActivities = fakeDataBase.GetUserActivities(firstUserId);
            Assert.AreEqual(3, userActivities.Count);

            ////Add account
            //var addAccRes = accountService.Add(Utilites.GenerateAccount());
            //Assert.IsFalse(addAccRes.HasError);
            //userActivities = fakeDataBase.GetUserActivities(firstUserId);
            //Assert.AreEqual(4, userActivities.Count);
            //Assert.IsTrue(userActivities.Exists(curAct => curAct.Type == UserActivityType.AddAccount && curAct.LinkedObjectId.Value == addAccRes.Value));

            ////Edit account
            //var acc = Utilites.GenerateAccount();
            //acc.Id = addAccRes.Value;
            //var editAccRes = accountService.Edit(acc);
            //Assert.IsFalse(editAccRes.HasError);
            //userActivities = fakeDataBase.GetUserActivities(firstUserId);
            //Assert.AreEqual(5, userActivities.Count);
            //Assert.IsTrue(userActivities.Exists(curAct => curAct.Type == UserActivityType.EditAccount && curAct.LinkedObjectId.Value == addAccRes.Value));

            ////Remove account
            //var removeAccRes = accountService.Remove(addAccRes.Value);
            //Assert.IsFalse(removeAccRes.HasError);
            //userActivities = fakeDataBase.GetUserActivities(firstUserId);
            //Assert.AreEqual(6, userActivities.Count);
            //Assert.IsTrue(userActivities.Exists(curAct => curAct.Type == UserActivityType.RemoveAccount && curAct.LinkedObjectId.Value == addAccRes.Value));

            //TODO: add disable account

        }

        [TestMethod]
        public void GetActivity()
        {
            membershipService.LogIn(firstUserId);
            //var userAct = Utilites.GenerateUserActivity();
            //userAct.Type = UserActivityType.AddAccount;
            //userAct.Date = new DateTime(2011, 6, 1);
            //fakeDataBase.AddUserActivity(firstUserId, userAct);
            //userAct = Utilites.GenerateUserActivity();
            //userAct.Type = UserActivityType.EditAccount;
            //userAct.Date = new DateTime(2011, 6, 2);
            //fakeDataBase.AddUserActivity(firstUserId, userAct);
            //userAct = Utilites.GenerateUserActivity();
            //userAct.Type = UserActivityType.RemoveAccount;
            //userAct.Date = new DateTime(2011, 6, 3);
            //fakeDataBase.AddUserActivity(firstUserId, userAct);
            //userAct = Utilites.GenerateUserActivity();
            //userAct.Type = UserActivityType.AddTransaction;
            //userAct.Date = new DateTime(2011, 6, 4);
            //fakeDataBase.AddUserActivity(firstUserId, userAct);
            //userAct = Utilites.GenerateUserActivity();
            //userAct.Type = UserActivityType.EditTransaction;
            //userAct.Date = new DateTime(2011, 6, 5);
            //fakeDataBase.AddUserActivity(firstUserId, userAct);
            //membershipService.LogOut();

            ////Check permissions
            //var getRes = activityService.Get(UserActivityType.LogIn);
            //Assert.IsTrue(getRes.HasError);
            //Assert.AreEqual(ErrorNumber.AccessDenied, getRes.Error);

            ////Get by type
            //membershipService.LogIn(firstUserId);
            //getRes = activityService.Get(UserActivityType.LogIn);
            //Assert.IsFalse(getRes.HasError);
            //Assert.AreEqual(2, getRes.Value.Count);

            ////Get last activites
            //getRes = activityService.Get(4);
            //Assert.IsFalse(getRes.HasError);
            //Assert.AreEqual(4, getRes.Value.Count);
            //Assert.AreEqual(new DateTime(2011, 6, 5), getRes.Value[2].Date);
            //Assert.AreEqual(new DateTime(2011, 6, 4), getRes.Value[3].Date);

            ////Get from date to date
            //membershipService.LogIn(firstUserId);
            //getRes = activityService.Get(new DateTime(2011, 6, 2), new DateTime(2011, 6, 4));
            //Assert.IsFalse(getRes.HasError);
            //Assert.AreEqual(3, getRes.Value.Count);

        }
    }
}

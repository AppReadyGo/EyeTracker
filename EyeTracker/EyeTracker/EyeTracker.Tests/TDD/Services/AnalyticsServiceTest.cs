using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EyeTracker.Tests.FakeData;
using EyeTracker.Core.Services;

namespace EyeTracker.Tests.TDD.Services
{
    [TestClass]
    public class AnalyticsServiceTest
    {
        private static IFakeMembershipService membershipService = null;

        private IFakeDataBase fakeDataBase = null;

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
        public void GetClickHeatMapData()
        {

        }
        [TestMethod]
        public void GetViewHeatMapData()
        {

        }
        [TestMethod]
        public void AddVisitInfo()
        {

        }
        [TestMethod]
        public void AddViewPartInfo()
        {

        }
        [TestMethod]
        public void AddClickInfo()
        {

        }
        [TestMethod]
        public void GetAnalyticsInfo()
        {

        }
        [TestMethod]
        public void GetClientId()
        {

        }
        [TestMethod]
        public void GetApplicationId()
        {

        }
        [TestMethod]
        public void ClearAnalytics()
        {

        }
    }
}

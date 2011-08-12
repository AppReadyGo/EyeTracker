using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Domain;

namespace EyeTracker.Tests.FakeData
{
    class FakeDataBase : IFakeDataBase
    {
        private int applId = 0;

        public Dictionary<string, List<UserActivity>> UserActivities = new Dictionary<string, List<UserActivity>>();

        public void Clear(List<string> userIds)
        {
            UserActivities = new Dictionary<string, List<UserActivity>>();
        }

        public void AddUserActivity(string userId, UserActivity userAct)
        {
            if (!UserActivities.ContainsKey(userId))
            {
                UserActivities[userId] = new List<UserActivity>();
            }
            UserActivities[userId].Add(userAct);
        }

        public List<UserActivity> GetUserActivities(string userId)
        {
            if (UserActivities.ContainsKey(userId))
                return UserActivities[userId];
            else
                return new List<UserActivity>();
        }

    }
}

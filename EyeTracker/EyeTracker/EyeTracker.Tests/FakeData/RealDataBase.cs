using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.EntityModels;

namespace EyeTracker.Tests.FakeData
{
    class RealDataBase : IFakeDataBase
    {
        public void Clear(List<string> userIds)
        {
            var e = new Entities("name=EyeTrackerEntities");
            foreach (string userId in userIds)
            {
                var gUserId = new Guid(userId);
                var activities = e.UserActivities.Where(curItem => curItem.UserId == gUserId);
                foreach (var curAct in activities)
                {
                    e.UserActivities.DeleteObject(curAct);
                }
            }
            e.SaveChanges();
        }

        public void AddUserActivity(string userId, UserActivity userAct)
        {
            var e = new Entities("name=EyeTrackerEntities");
            userAct.UserId = new Guid(userId);
            e.AddToUserActivities(userAct);
            e.SaveChanges();
        }

        public List<UserActivity> GetUserActivities(string userId)
        {
            var e = new Entities("name=EyeTrackerEntities");
            return e.UserActivities.Where(curItem => curItem.UserId == new Guid(userId)).ToList();
        }
    }
}

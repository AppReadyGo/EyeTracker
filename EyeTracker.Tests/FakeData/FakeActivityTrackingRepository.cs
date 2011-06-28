using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.DAL.Models;

namespace EyeTracker.Tests.FakeData
{
    class FakeActivityTrackingRepository : IActivityTrackingRepository
    {
        FakeDataBase fakeDataBase = null;

        public FakeActivityTrackingRepository()
        {
            fakeDataBase = (FakeDataBase)Utilites.Container.Resolve<IFakeDataBase>();
        }

        public void Write(string userId, UserActivityType userActivityType, string description, int? linkedObjectId)
        {
            fakeDataBase.AddUserActivity(userId, new UserActivity()
            {
                Date = DateTime.UtcNow,
                Description = description,
                Type = userActivityType,
                LinkedObjectId = linkedObjectId
            });
        }

        public List<UserActivity> Get(string userId, UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount)
        {
            if (fakeDataBase.UserActivities.ContainsKey(userId))
            {
                var res = fakeDataBase.UserActivities[userId].Where(curItem =>
                    (!fromDate.HasValue || curItem.Date >= fromDate.Value) &&
                    (!toDate.HasValue || curItem.Date <= toDate.Value) &&
                    (!userActivityType.HasValue || curItem.Type == userActivityType));
                return lastActivitesCount.HasValue ? res.Reverse().Take(lastActivitesCount.Value).ToList() : res.ToList();
            }
            return new List<UserActivity>();
        }
    }
}

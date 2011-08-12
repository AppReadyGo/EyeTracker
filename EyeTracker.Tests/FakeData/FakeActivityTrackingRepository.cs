using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.DAL.Domain;

namespace EyeTracker.Tests.FakeData
{
    class FakeActivityTrackingRepository : IActivityTrackingRepository
    {
        FakeDataBase fakeDataBase = null;

        public FakeActivityTrackingRepository()
        {
            fakeDataBase = (FakeDataBase)Utilites.Container.Resolve<IFakeDataBase>();
        }

        public void Write(UserActivity userActivity)
        {
            fakeDataBase.AddUserActivity(userActivity.UserId.ToString(), userActivity);
        }

        public List<UserActivity> Get(Guid userId, UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount)
        {
            if (fakeDataBase.UserActivities.ContainsKey(userId.ToString()))
            {
                var res = fakeDataBase.UserActivities[userId.ToString()].Where(curItem =>
                    (!fromDate.HasValue || curItem.Date >= fromDate.Value) &&
                    (!toDate.HasValue || curItem.Date <= toDate.Value) &&
                    (!userActivityType.HasValue || curItem.ActivityType == userActivityType));
                return lastActivitesCount.HasValue ? res.Reverse().Take(lastActivitesCount.Value).ToList() : res.ToList();
            }
            return new List<UserActivity>();
        }
    }
}

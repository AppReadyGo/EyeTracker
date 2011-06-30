using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Models;
using EyeTracker.DAL.EntityModels;

namespace EyeTracker.DAL
{
    public interface IActivityTrackingRepository
    {
        void Write(UserActivity userActivity);

        List<UserActivity> Get(Guid userId, UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount);
    }

    public class ActivityTrackingRepository : IActivityTrackingRepository
    {

        public void Write(UserActivity userActivity)
        {
            var e = new Entities("name=EyeTrackerEntities");
            e.AddToUserActivities(userActivity);
            e.SaveChanges();
        }

        public List<UserActivity> Get(Guid userId, UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount)
        {
            var e = new Entities("name=EyeTrackerEntities");
            var res = e.UserActivities.Where(curItem => curItem.UserId == userId &&
                (!fromDate.HasValue || curItem.Date >= fromDate.Value) &&
                (!toDate.HasValue || curItem.Date <= toDate.Value) &&
                (!userActivityType.HasValue || curItem.ActivityType == userActivityType));
            return lastActivitesCount.HasValue ? res.Reverse().Take(lastActivitesCount.Value).ToList() : res.ToList();
        }
    }
}

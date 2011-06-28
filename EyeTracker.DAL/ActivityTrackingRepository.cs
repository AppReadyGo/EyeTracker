using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Models;

namespace EyeTracker.DAL
{
    public interface IActivityTrackingRepository
    {
        void Write(string userId, UserActivityType userActivityType, string description, int? linkedObjectId);

        List<UserActivity> Get(string userId, UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount);
    }

    public class ActivityTrackingRepository : IActivityTrackingRepository
    {

        public void Write(string userId, UserActivityType userActivityType, string description, int? linkedObjectId)
        {
            throw new NotImplementedException();
        }

        public List<UserActivity> Get(string userId, UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount)
        {
            throw new NotImplementedException();
        }
    }
}

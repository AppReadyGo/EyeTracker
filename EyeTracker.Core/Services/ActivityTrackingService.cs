using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common;
using EyeTracker.DAL.Domain;

namespace EyeTracker.Core.Services
{
    public interface IActivityTrackingService
    {
        OperationResult Write(UserActivityType userActivityType, string description);

        OperationResult Write(UserActivityType userActivityType, string description, int? linkedObjectId);

        OperationResult<List<UserActivity>> Get(UserActivityType userActivityType);

        OperationResult<List<UserActivity>> Get(DateTime fromDate, DateTime toDate);

        OperationResult<List<UserActivity>> Get(int lastActivitesCount);

        OperationResult<List<UserActivity>> Get(UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount);
    }

    public class ActivityTrackingService : IActivityTrackingService
    {
        ActivityTracking tracking;

        public ActivityTrackingService() : this(new ActivityTracking()) { }

        public ActivityTrackingService(ActivityTracking tracking)
        {
            this.tracking = tracking;
        }

        public OperationResult Write(UserActivityType userActivityType, string description)
        {
            return Write(userActivityType, description, null);
        }

        public OperationResult Write(UserActivityType userActivityType, string description, int? linkedObjectId)
        {
            return tracking.Write(new UserActivity() { 
                Date = DateTime.UtcNow,
                UserId = ObjectContainer.Instance.CurrentUserDetails.Id,
                ActivityType = userActivityType,
                Description = description,
                LinkedObjectId = linkedObjectId
            });
        }

        public OperationResult<List<UserActivity>> Get(UserActivityType userActivityType)
        {
            return Get(userActivityType, null, null, null);
        }

        public OperationResult<List<UserActivity>> Get(DateTime fromDate, DateTime toDate)
        {
            return Get(null, fromDate, toDate, null);
        }

        public OperationResult<List<UserActivity>> Get(int lastActivitesCount)
        {
            return Get(null, null, null, lastActivitesCount);
        }

        public OperationResult<List<UserActivity>> Get(UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount)
        {
            return tracking.Get(ObjectContainer.Instance.CurrentUserDetails.Id, userActivityType, fromDate, toDate, lastActivitesCount);
        }
    }
}

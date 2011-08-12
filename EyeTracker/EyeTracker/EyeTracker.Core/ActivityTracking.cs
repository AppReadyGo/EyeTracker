using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.Common;
using EyeTracker.DAL.Domain;

namespace EyeTracker.Core
{
    public class ActivityTracking
    {
        private IActivityTrackingRepository repository;

        public ActivityTracking() : this(new ActivityTrackingRepository()) { }

        public ActivityTracking(IActivityTrackingRepository repository)
        {
            this.repository = repository;
        }

        public OperationResult Write(string userId, UserActivityType userActivityType, string description, int? linkedObjectId)
        {
            return Write(new UserActivity() { Date = DateTime.UtcNow, UserId = new Guid(userId), ActivityType = userActivityType, Description = description, LinkedObjectId = linkedObjectId });
        }

        public OperationResult Write(UserActivity userActivity)
        {
            try
            {
                if (!string.IsNullOrEmpty(userActivity.Description) && userActivity.Description.Length > 1000)
                {
                    return new OperationResult(ErrorNumber.WrongDescription);
                }
                repository.Write(userActivity);
                return new OperationResult(ErrorNumber.None);
            }
            catch (Exception exp)
            {
                return new OperationResult(exp, "Error in call: ActivityTracking.Write(userActivityType:{0}, description:{1}), linkedObjectId:{2}", userActivity.ActivityType, userActivity.Description, userActivity.LinkedObjectId);
            }
        }

        public OperationResult<List<UserActivity>> Get(string userId, UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount)
        {
            try
            {
                return new OperationResult<List<UserActivity>>(repository.Get(new Guid(userId), userActivityType, fromDate, toDate, lastActivitesCount));
            }
            catch (Exception exp)
            {
                return new OperationResult<List<UserActivity>>(exp, "Error in call: ActivityTracking.Get(userActivityType:{0}, fromDate:{1}, toDate:{2}, lastActivitesCount:{3})", userActivityType, fromDate, toDate, lastActivitesCount);
            }
        }
    }
}

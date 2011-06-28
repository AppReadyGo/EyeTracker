using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL;
using EyeTracker.Common;
using EyeTracker.DAL.Models;

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
            try
            {
                if (!string.IsNullOrEmpty(description) && description.Length > 1000)
                {
                    return new OperationResult(ErrorNumber.WrongDescription);
                }
                repository.Write(userId, userActivityType, description, linkedObjectId);
                return new OperationResult(ErrorNumber.None);
            }
            catch (Exception exp)
            {
                return new OperationResult(exp, "Error in call: ActivityTracking.Write(userActivityType:{0}, description:{1}), linkedObjectId:{2}", userActivityType, description, linkedObjectId);
            }
        }

        public OperationResult<List<UserActivity>> Get(string userId, UserActivityType? userActivityType, DateTime? fromDate, DateTime? toDate, int? lastActivitesCount)
        {
            try
            {
                return new OperationResult<List<UserActivity>>(repository.Get(userId, userActivityType, fromDate, toDate, lastActivitesCount));
            }
            catch (Exception exp)
            {
                return new OperationResult<List<UserActivity>>(exp, "Error in call: ActivityTracking.Get(userActivityType:{0}, fromDate:{1}, toDate:{2}, lastActivitesCount:{3})", userActivityType, fromDate, toDate, lastActivitesCount);
            }
        }
    }
}

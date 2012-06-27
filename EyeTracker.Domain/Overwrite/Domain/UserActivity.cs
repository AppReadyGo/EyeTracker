using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.DAL.Domain
{
    public class UserActivity
    {
        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public UserActivityType ActivityType { get; set; }

        public string Description { get; set; }

        public int? LinkedObjectId { get; set; }
    }
}

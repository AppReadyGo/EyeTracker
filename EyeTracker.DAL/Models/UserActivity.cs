using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.DAL.Models
{
    public class UserActivity
    {
        public DateTime Date { get; set; }
        public UserActivityType Type { get; set; }
        public string Description { get; set; }
        public int? LinkedObjectId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using EyeTracker.DAL.Models;

namespace EyeTracker.DAL.EntityModels
{
    public partial class UserActivity : EntityObject
    {
        public UserActivityType ActivityType
        {
            get
            {
                return (UserActivityType)Type;
            }
            set
            {
                Type = (Int16)value;
            }
        }
    }
}

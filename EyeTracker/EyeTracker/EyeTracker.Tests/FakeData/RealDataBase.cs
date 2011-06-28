using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Tests.FakeData
{
    class RealDataBase : IFakeDataBase
    {
        public void Clear(List<string> userIds)
        {
            throw new NotImplementedException();
        }

        public void AddUserActivity(string userId, DAL.Models.UserActivity userAct)
        {
            throw new NotImplementedException();
        }

        public List<DAL.Models.UserActivity> GetUserActivities(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Domain;

namespace EyeTracker.Tests.FakeData
{
    interface IFakeDataBase
    {
        void AddUserActivity(string userId, UserActivity userAct);
        List<UserActivity> GetUserActivities(string userId);
        void Clear(List<string> userIds);
    }
}

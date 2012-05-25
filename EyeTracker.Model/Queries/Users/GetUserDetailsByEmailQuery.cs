using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Users;

namespace EyeTracker.Common.Queries.Users
{
    public class GetUserDetailsByEmailQuery : IQuery<UserDetailsResult>
    {
        public string Email { get; private set; }

        public GetUserDetailsByEmailQuery(string email)
        {
            this.Email = email;
        }
    }
}

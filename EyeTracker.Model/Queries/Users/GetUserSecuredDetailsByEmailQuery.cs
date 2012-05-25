using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Users;

namespace EyeTracker.Common.Queries.Users
{
    public class GetUserSecuredDetailsByEmailQuery : IQuery<UserSecuredDetailsResult>
    {
        public string Email { get; private set; }

        public GetUserSecuredDetailsByEmailQuery(string email)
        {
            this.Email = email;
        }
    }
}

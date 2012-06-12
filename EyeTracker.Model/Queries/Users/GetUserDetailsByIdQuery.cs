using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Users;

namespace EyeTracker.Common.Queries.Users
{
    public class GetUserDetailsByIdQuery : IQuery<UserDetailsResult>
    {
        public int Id { get; private set; }

        public GetUserDetailsByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}

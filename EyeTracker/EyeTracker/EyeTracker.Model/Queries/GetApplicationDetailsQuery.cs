using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Common.Queries;
using EyeTracker.Common.QueryResults;

namespace EyeTracker.Common.Queries.Users
{
    public class GetApplicationDetailsQuery : IQuery<ApplicationDetailsResult>
    {
        public int Id { get; private set; }

        public GetApplicationDetailsQuery(int id)
        {
            this.Id = id;
        }
    }
}

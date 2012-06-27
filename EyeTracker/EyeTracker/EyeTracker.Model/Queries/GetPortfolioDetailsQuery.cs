using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Users;
using EyeTracker.Common.Queries;
using EyeTracker.Common.QueryResults;

namespace EyeTracker.Common.Queries.Users
{
    public class GetPortfolioDetailsQuery : IQuery<PortfolioDetailsResult>
    {
        public int Id { get; private set; }

        public GetPortfolioDetailsQuery(int id)
        {
            this.Id = id;
        }
    }
}

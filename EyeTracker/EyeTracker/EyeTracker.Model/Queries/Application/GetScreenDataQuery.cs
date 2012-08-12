using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Common.Queries.Application
{
    public class GetScreenDataQuery : IQuery<ScreenDataResult>
    {
        public int Id { get; private set; }

        public GetScreenDataQuery(int id)
        {
            this.Id = id;
        }
    }
}

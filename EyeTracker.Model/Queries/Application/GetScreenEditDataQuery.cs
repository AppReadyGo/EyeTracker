using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Common.Queries.Application
{
    public class GetScreenEditDataQuery : IQuery<ScreenDetailsDataResult>
    {
        public int Id { get; private set; }

        public GetScreenEditDataQuery(int id)
        {
            this.Id = id;
        }
    }
}

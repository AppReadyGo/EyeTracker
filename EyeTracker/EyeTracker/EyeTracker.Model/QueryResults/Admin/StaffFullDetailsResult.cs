using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Users
{
    public class StaffFullDetailsResult : UserFullDetailsResult
    {
        public IEnumerable<StaffRole> Roles { get; set; }
    }
}

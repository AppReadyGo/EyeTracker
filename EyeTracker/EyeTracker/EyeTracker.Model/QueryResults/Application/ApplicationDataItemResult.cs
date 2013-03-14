using EyeTracker.Common.Entities;

namespace EyeTracker.Common.QueryResults.Application
{
    public class ApplicationDataItemResult : ApplicationResult
    {
        public int Visits { get; set; }

        public ApplicationType Type { get; set; }

        public bool IsActive { get; set; }
    }
}

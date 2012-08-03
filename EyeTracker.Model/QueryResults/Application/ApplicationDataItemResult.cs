using EyeTracker.Common.Entities;

namespace EyeTracker.Common.QueryResults.Application
{
    public class ApplicationDataItemResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Visits { get; set; }

        public ApplicationType Type { get; set; }

        public bool IsActive { get; set; }
    }
}

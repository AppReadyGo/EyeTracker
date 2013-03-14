using EyeTracker.Common.Entities;

namespace EyeTracker.Common.QueryResults
{
    public class ApplicationDetailsResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ApplicationType Type { get; set; }
    }
}

using EyeTracker.Common.Entities;

namespace EyeTracker.Common.QueryResults.Application
{
    public class ScreenDataItemResult
    {
        public int Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Path { get; set; }

        public string FileExtension { get; set; }
    }
}

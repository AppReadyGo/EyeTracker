using EyeTracker.Common.Entities;
using System.Drawing;

namespace EyeTracker.Common.QueryResults.Application
{
    public class ApplicationScreenResult
    {
        public int Id { get; set; }

        public Size ScreenSize { get; set; }

        public string Path { get; set; }

        public int ApplicationId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Application
{
    public class ScreenDetailsResult
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string FileExtention { get; set; }

        public string PortfolioDescription { get; set; }

        public string ApplicationDescription { get; set; }

        public int ApplicationId { get; set; }

        public int PortfolioId { get; set; }
    }
}

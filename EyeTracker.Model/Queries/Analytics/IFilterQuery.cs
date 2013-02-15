using System;
using System.Drawing;
namespace EyeTracker.Common.Queries.Analytics
{
    public interface IFilterQuery
    {
        int ApplicationId { get; }

        string City { get; }

        string Country { get; }

        DateTime From { get; }

        string Language { get; }

        string OperatingSystem { get; }

        string Path { get; }

        Size? ScreenSize { get; }

        DateTime To { get; }
    }
}

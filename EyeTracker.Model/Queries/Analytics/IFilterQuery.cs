using System;
namespace EyeTracker.Common.Queries.Analytics
{
    public interface IFilterQuery
    {
        int? ApplicationId { get; }
        string City { get; }
        string Country { get; }
        DateTime From { get; }
        string Language { get; }
        string OperatingSystem { get; }
        string Path { get; }
        int? PortfolioId { get; }
        System.Drawing.Size? ScreenSize { get; }
        DateTime To { get; }
    }
}

using EyeTracker.Common.QueryResults.Content;

namespace EyeTracker.Common.Queries.Content
{
    public class GetThemeQuery : IQuery<ThemeResult>
    {
        public string Url { get; private set; }

        public GetThemeQuery(string url)
        {
            this.Url = url;
        }
    }
}

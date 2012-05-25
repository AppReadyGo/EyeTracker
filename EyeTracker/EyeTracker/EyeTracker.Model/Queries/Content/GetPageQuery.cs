using EyeTracker.Common.QueryResults.Content;

namespace EyeTracker.Common.Queries.Content
{
    public class GetPageQuery : IQuery<PageResult>
    {
        public string Url { get; private set; }

        public GetPageQuery(string url)
        {
            this.Url = url;
        }
    }
}

using EyeTracker.Common.QueryResults.Content;

namespace EyeTracker.Common.Queries.Content
{
    public class GetKeyQuery : IQuery<KeyResult>
    {
        public string Url { get; private set; }

        public GetKeyQuery(string url)
        {
            this.Url = url;
        }
    }
}

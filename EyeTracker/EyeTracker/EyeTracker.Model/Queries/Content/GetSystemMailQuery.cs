using EyeTracker.Common.QueryResults.Content;

namespace EyeTracker.Common.Queries.Content
{
    public class GetSystemMailQuery : IQuery<MailResult>
    {
        public string Url { get; private set; }

        public GetSystemMailQuery(string url)
        {
            this.Url = url;
        }
    }
}

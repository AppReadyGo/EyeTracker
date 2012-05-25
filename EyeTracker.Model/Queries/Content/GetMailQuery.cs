using EyeTracker.Common.QueryResults.Content;

namespace EyeTracker.Common.Queries.Content
{
    public class GetMailQuery : IQuery<MailResult>
    {
        public string Url { get; private set; }

        public GetMailQuery(string url)
        {
            this.Url = url;
        }
    }
}

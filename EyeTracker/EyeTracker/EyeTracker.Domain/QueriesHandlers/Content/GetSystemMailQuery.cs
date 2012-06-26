using System.Linq;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Common.QueryResults.Content;
using EyeTracker.Domain.Model.Content;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries
{
    public class GetSystemMailQueryHandler : IQueryHandler<GetSystemMailQuery, MailResult>
    {
        public MailResult Run(NHibernate.ISession session, GetSystemMailQuery query)
        {
            var mail = session.Query<SystemMail>()
                            .Where(m => m.Url.ToLower() == query.Url.ToLower())
                            .Select(m => m)
                            .Single();
            return new MailResult
            {
                Id = mail.Id,
                Url = mail.Url,
                Body = mail.Body.Value,
                Subject = mail.Subject.Value,
                ThemeUrl = mail.Theme.Url
            };
        }
    }
}

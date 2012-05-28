using System.Linq;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Common.QueryResults.Content;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries
{
    public class GetMailQueryHandler : IQueryHandler<GetMailQuery, MailResult>
    {
        public MailResult Run(ISession session, GetMailQuery query)
        {
            var mail = session.Query<Mail>()
                            .Where(m => m.Url.ToLower() == query.Url.ToLower())
                            .Select(m => new MailResult
                            {
                                Id = m.Id,
                                Url = m.Url,
                                ThemeUrl = m.Theme.Url
                            })
                            .SingleOrDefault();

            var items = session.Query<Mail>()
                            .Where(m => m.Id == mail.Id)
                            .SelectMany(m => m.Items)
                            .Select(i => new { i.SubKey, i.Value })
                            .ToArray();

            mail.Body = items.Single(i => i.SubKey.ToLower() == "body").Value;
            mail.Subject = items.Single(i => i.SubKey.ToLower() == "subject").Value;

            return mail;
        }
    }
}

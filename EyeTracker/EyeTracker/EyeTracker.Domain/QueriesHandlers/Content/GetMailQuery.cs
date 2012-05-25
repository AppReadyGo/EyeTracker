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
            return session.Query<Mail>()
                            .Where(m => m.Url.ToLower() == query.Url.ToLower())
                            .Select(m => new MailResult
                            {
                                Id = m.Id,
                                Url = m.Url,
                                Body = m.Body.Value,
                                Subject = m.Subject.Value,
                                ThemeUrl = m.Theme.Url
                            })
                            .SingleOrDefault();
        }
    }
}

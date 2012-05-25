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
            return session.Query<SystemMail>()
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

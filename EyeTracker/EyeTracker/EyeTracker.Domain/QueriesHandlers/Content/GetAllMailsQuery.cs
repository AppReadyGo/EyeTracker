using System.Collections.Generic;
using System.Linq;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries
{
    public class GetAllMailsQueryHandler : IQueryHandler<GetAllMailsQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        public IEnumerable<KeyValuePair<int, string>> Run(ISession session, GetAllMailsQuery query)
        {
            return session.Query<Mail>()
                          .Select(m => new KeyValuePair<int, string>(m.Id, m.Url))
                          .ToArray();
        }
    }
}

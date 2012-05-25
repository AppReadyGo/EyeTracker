using System.Collections.Generic;
using System.Linq;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries
{
    public class GetAllPagesQueryHandler : IQueryHandler<GetAllPagesQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        public IEnumerable<KeyValuePair<int, string>> Run(NHibernate.ISession session, GetAllPagesQuery query)
        {
            return session.Query<Page>()
                          .Select(p => new KeyValuePair<int, string>(p.Id, p.Url))
                          .ToArray();
        }
    }
}

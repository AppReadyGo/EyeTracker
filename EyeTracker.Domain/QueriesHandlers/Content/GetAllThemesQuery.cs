using System.Collections.Generic;
using System.Linq;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries
{
    public class GetAllThemesQueryHandler : IQueryHandler<GetAllThemesQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        public IEnumerable<KeyValuePair<int, string>> Run(ISession session, GetAllThemesQuery query)
        {
            return session.Query<Theme>()
                          .Select(t => new KeyValuePair<int, string>(t.Id, t.Name))
                          .ToArray();
        }
    }
}

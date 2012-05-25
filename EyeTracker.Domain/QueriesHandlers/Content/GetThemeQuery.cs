using System.Linq;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Common.QueryResults.Content;
using EyeTracker.Domain.Model.Content;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries
{
    public class GetThemeQueryHandler : IQueryHandler<GetThemeQuery, ThemeResult>
    {
        public ThemeResult Run(NHibernate.ISession session, GetThemeQuery query)
        {
            return session.Query<Theme>()
                            .Where(t => t.Url.ToLower() == query.Url.ToLower())
                            .Select(t => new ThemeResult
                            {
                                Id = t.Id,
                                Url = t.Url,
                                Name = t.Name,
                                Type = t.Type
                            })
                            .SingleOrDefault();
        }
    }
}

using System.Linq;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Common.QueryResults.Content;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries
{
    public class GetPageQueryHandler : IQueryHandler<GetPageQuery, PageResult>
    {
        public PageResult Run(ISession session, GetPageQuery query)
        {
            return session.Query<Page>()
                            .Where(p => p.Url.ToLower() == query.Url.ToLower())
                            .Select(p => new PageResult
                            {
                                Id = p.Id,
                                Url = p.Url,
                                Title = p.Title.Value,
                                Content = p.Content.Value,
                                ThemeUrl = p.Theme.Url
                            })
                            .SingleOrDefault();
        }
    }
}

using System.Linq;
using EyeTracker.Common.Queries.Content;
using EyeTracker.Common.QueryResults.Content;
using EyeTracker.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.Queries
{
    public class GetKeyQueryHandler : IQueryHandler<GetKeyQuery, KeyResult>
    {
        public KeyResult Run(ISession session, GetKeyQuery query)
        {
            var key = session.Query<Key>()
                            .Where(k => k.Url.ToLower() == query.Url.ToLower())
                            .Select(k => new KeyResult
                            {
                                Id = k.Id,
                                Url = k.Url,
                            })
                            .SingleOrDefault();

            if (key != null)
            {
                key.Items = session.Query<Key>()
                                .Where(k => k.Id == key.Id)
                                .SelectMany(k => k.Items)
                                .Select(i => new ItemResult
                                { 
                                    IsHTML = i.IsHTML,
                                    Value = i.Value
                                })
                                .ToArray();
            }

            return key;
        }
    }
}

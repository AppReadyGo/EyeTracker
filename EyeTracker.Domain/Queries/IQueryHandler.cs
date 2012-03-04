using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace EyeTracker.Domain.Queries
{
    public interface IQueryHandler<TQuery, TResult>
    {
        TResult Run(ISession session, TQuery query);
    }
}

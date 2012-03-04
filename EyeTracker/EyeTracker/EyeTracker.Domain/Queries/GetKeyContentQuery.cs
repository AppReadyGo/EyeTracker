﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Queries;
using EyeTracker.Domain.CommandHandlers;
using EyeTracker.Domain.Model;

using NHibernate.Linq;
using NHibernate;

namespace EyeTracker.Domain.Queries
{
    public class GetKeyContentQuery : IQueryHandler<GetKeyContent, Dictionary<string, string>>
    {
        private IRepository repository;

        public GetKeyContentQuery(IRepository repository)
        {
            this.repository = repository;
        }

        public Dictionary<string, string> Run(ISession session, GetKeyContent query)
        {
            return session.Query<ContentItem>()
                    .Where(c => c.Key == query.Key)
                    .Select(c => new { SubKey = c.SubKey, Value = c.Value })
                    .ToList()
                    .ToDictionary(k => k.SubKey, v => v.Value);
        }
    }
}

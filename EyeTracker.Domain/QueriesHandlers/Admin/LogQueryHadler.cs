using System.Linq;
using EyeTracker.Common.Queries.Admin;
using EyeTracker.Common.Results.Admin;
using EyeTracker.Domain.Model.BackOffice;
using EyeTracker.Domain.Queries;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;

namespace EyeTracker.Domain.QueriesHandlers.Admin
{

    public class LogQueryHandler : IQueryHandler<LogDataQuery, LogDataResult>
    {
        public LogDataResult Run(ISession session, LogDataQuery query)
        {
            var res = new LogDataResult();

            var logQuery = session.Query<Log>();

            if (!string.IsNullOrEmpty(query.SearchStr))
            {
                logQuery = logQuery.Where(a => a.FormattedMessage.ToLower().Contains(query.SearchStr.ToLower()));
            }

            if (query.FromDate.HasValue)
            {
                logQuery = logQuery.Where(a => a.Timestamp >= query.FromDate.Value);
            }

            if (query.ToDate.HasValue)
            {
                logQuery = logQuery.Where(a => a.Timestamp <= query.ToDate.Value);
            }

            if (query.CategoryId.HasValue)
            {
                logQuery = logQuery.Where(a => a.Categories.Any(c => c.Id == query.CategoryId.Value));
            }

            if (!string.IsNullOrEmpty(query.Severity))
            {
                logQuery = logQuery.Where(a => a.Severity.ToLower() == query.Severity.ToLower());
            }

            if (!string.IsNullOrEmpty(query.ThreadId))
            {
                logQuery = logQuery.Where(a => a.Win32ThreadId == query.ThreadId);
            }

            if (!string.IsNullOrEmpty(query.ProcessId))
            {
                logQuery = logQuery.Where(a => a.ProcessID == query.ProcessId);
            }

            if (query.CategoryId.HasValue)
            {
                logQuery = logQuery.Where(a => a.Categories.Any(c => c.Id == query.CategoryId.Value));
            }

            res.Log = logQuery.Select(a => new LogResult
            {
                Id = a.Id,
                EventID = a.EventID,
                Priority = a.Priority,
                Severity = a.Severity,
                Title = a.Title,
                Timestamp = a.Timestamp,
                MachineName = a.MachineName,
                AppDomainName = a.AppDomainName,
                ProcessID = a.ProcessID,
                ProcessName = a.ProcessName,
                ThreadName = a.ThreadName,
                Win32ThreadId = a.Win32ThreadId,
                Message = a.Message,
                FormattedMessage = a.FormattedMessage
            })
                        .OrderByDescending(a => a.Timestamp)
                        .ToArray();

            var auditIds = res.Log.Select(a => a.Id).ToArray();

            Category category = null;
            LogCategory auditCategory = null;
            var categories = session.QueryOver<Log>()
                            .JoinAlias(a => a.Categories, () => category)
                            .Where(a => a.Id.IsIn(auditIds))
                            .SelectList(list => list
                            .Select(a => a.Id).WithAlias(() => auditCategory.LogId)
                            .Select(a => category.Name).WithAlias(() => auditCategory.CategoryName))
                            .TransformUsing(Transformers.AliasToBean<LogCategory>())
                            .List<LogCategory>();

            foreach (var a in res.Log)
            {
                a.Categories = categories.Where(c => c.LogId == a.Id).Select(x => x.CategoryName);
            }

            //Get data
            res.Categories = session.Query<Category>()
                        .Select(c => new { c.Id, c.Name })
                        .ToDictionary(k => k.Id, v => v.Name);
            res.Severities = session.Query<Log>()
                        .Select(a => a.Severity)
                        .ToArray()
                        .Distinct();
            return res;
        }

        private class LogCategory
        {
            public int LogId { get; set; }
            public string CategoryName { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;

namespace EyeTracker.Domain.Model.BackOffice
{
    public class Log
    {
        private Iesi.Collections.Generic.ISet<Category> categories;

        public virtual int Id { get; protected set; }
        public virtual int? EventID { get; protected set; }
        public virtual int Priority { get; protected set; }
        public virtual string Severity { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual DateTime Timestamp { get; protected set; }
        public virtual string MachineName { get; protected set; }
        public virtual string AppDomainName { get; protected set; }
        public virtual string ProcessID { get; protected set; }
        public virtual string ProcessName { get; protected set; }
        public virtual string ThreadName { get; protected set; }
        public virtual string Win32ThreadId { get; protected set; }
        public virtual string Message { get; protected set; }
        public virtual string FormattedMessage { get; protected set; }
        public virtual IEnumerable<Category> Categories { get { return categories; } }
    }
}

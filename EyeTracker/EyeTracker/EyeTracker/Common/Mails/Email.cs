using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeTracker.Common.Mails
{
    public abstract class Email
    {
        public string EmailPagePath { get; private set; }
        public object Model { get; protected set; }
        public IEnumerable<string> To { get; protected set; }
        public IEnumerable<string> Cc { get; protected set; }
        public IEnumerable<string> Bcc { get; protected set; }
        public string Subject { get; protected set; }

        protected Email(string emailPagePath)
        {
            this.EmailPagePath = emailPagePath;
        }

        protected Email(string emailPagePath, object model, string subject, IEnumerable<string> to, IEnumerable<string> cc = null, IEnumerable<string> bcc = null)
            : this(emailPagePath)
        {
            this.EmailPagePath = emailPagePath;
            this.Model = model;
            this.Subject = subject;
            this.To = to;
            this.Cc = cc;
            this.Bcc = bcc;
        }
    }

    public abstract class SystemEmail : Email
    {
        public SystemEmail()
            : base("~/Views/Mails/System.aspx")
        {
        }
    }
}
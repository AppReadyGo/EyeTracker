using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Content
{
    public enum EmailType
    {
        Base = 0
    }

    public abstract class Email
    {
        public virtual int Id { get; protected set; }
        public virtual string Subject { get; protected set; }
        public virtual string Body { get; protected set; }
        public abstract EmailType Type { get; }

        public Email() { }

        public Email(string subject, string body)
        {
            this.Subject = subject;
            this.Body = body;
        }
    }

    public class BaseEmail : Email
    {
        public override EmailType Type
        {
            get { return EmailType.Base; }
        }

        public BaseEmail() { }

        public BaseEmail(string subject, string body)
            : base(subject, body)
        {
        }
    }
}

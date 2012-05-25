
namespace EyeTracker.Domain.Model.Content
{
    public class Mail : SystemMail
    {
        public override bool IsSystem { get { return false; } }

        public Mail(string url, Theme theme, string subject, string body)
        {
            this.Url = url;
            this.Theme = theme;
            this.Subject = new Item("subject", subject, false);
            this.Body = new Item("body", body, true);
        }

        public virtual void Update(string url, Theme theme, string subject, string body)
        {
            this.Url = url;
            this.Theme = theme;
            this.Subject.Update(subject);
            this.Body.Update(body);
        }
    }

    public class SystemMail
    {
        public virtual int Id { get; protected set; }

        public virtual string Url { get; protected set; }

        public virtual Theme Theme { get; protected set; }

        public virtual bool IsSystem { get { return true; } }

        public virtual Item Subject { get; protected set; }

        public virtual Item Body { get; protected set; }

        /// <summary>
        /// System mail cannot be created programmatically just by script
        /// </summary>

        public virtual void Update(Theme theme, string subject, string body)
        {
            this.Theme = theme;
            this.Subject.Update(subject);
            this.Body.Update(body);
        }
    }
}

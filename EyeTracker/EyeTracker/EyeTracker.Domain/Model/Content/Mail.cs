using System.Linq;
using System.Collections.Generic;
using Iesi.Collections.Generic;
namespace EyeTracker.Domain.Model.Content
{
    public class Mail : SystemMail
    {
        public override bool IsSystem { get { return false; } }

        public Mail() { }

        public Mail(string url, Theme theme, string subject, string body)
        {
            this.Url = url;
            this.Theme = theme;
            this.items = new HashedSet<Item>(new[] { new Item("subject", subject, false), new Item("body", body, true) });
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
        protected Iesi.Collections.Generic.ISet<Item> items = null;
        private Item body = null;
        private Item subject = null;

        public virtual int Id { get; protected set; }

        public virtual string Url { get; protected set; }

        public virtual Theme Theme { get; protected set; }

        public virtual bool IsSystem { get { return true; } }

        public virtual Item Subject 
        {
            get
            {
                if (subject == null)
                {
                    this.InitItems();
                }
                return subject;
            }
        }

        public virtual Item Body
        {
            get
            {
                if (body == null)
                {
                    this.InitItems();
                }
                return body;
            }
        }

        public virtual IEnumerable<Item> Items { get { return items; } }

        /// <summary>
        /// System mail cannot be created programmatically just by script
        /// </summary>
        public SystemMail() { }

        public virtual void Update(Theme theme, string subject, string body)
        {
            this.Theme = theme;
            this.Subject.Update(subject);
            this.Body.Update(body);
        }

        private void InitItems()
        {
            this.body = this.items.Single(i => i.SubKey.ToLower() == "body");
            this.subject = this.items.Single(i => i.SubKey.ToLower() == "subject");
        }
    }
}

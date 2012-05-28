using System.Linq;
using System.Collections.Generic;
using Iesi.Collections.Generic;
namespace EyeTracker.Domain.Model.Content
{
    public class Page
    {
        protected Iesi.Collections.Generic.ISet<Item> items = null;
        private Item title = null;
        private Item content = null;

        public virtual int Id { get; protected set; }

        public virtual string Url { get; protected set; }

        public virtual Theme Theme { get; protected set; }

        public virtual Item Title
        {
            get
            {
                if (this.title == null)
                {
                    this.InitItems();
                }
                return title;
            }
        }

        public virtual Item Content
        {
            get
            {
                if (this.content == null)
                {
                    this.InitItems();
                }
                return content;
            }
        }

        public virtual IEnumerable<Item> Items { get { return this.items; } }

        public Page() { }

        public Page(string url, Theme theme, string title, string content)
        {
            this.Url = url;
            this.Theme = theme;
            this.items = new HashedSet<Item>(new[] { new Item("title", title, false), new Item("content", content, true) });
        }

        public virtual void Update(string url, Theme theme, string title, string content)
        {
            this.Url = url;
            this.Theme = theme;
            this.Title.Update(title);
            this.Content.Update(content);
        }

        private void InitItems()
        {
            this.title = this.items.Single(i => i.SubKey.ToLower() == "title");
            this.content = this.items.Single(i => i.SubKey.ToLower() == "content");
        }
    }
}

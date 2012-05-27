
namespace EyeTracker.Domain.Model.Content
{
    public class Page
    {
        public virtual int Id { get; protected set; }

        public virtual string Url { get; protected set; }

        public virtual Theme Theme { get; protected set; }

        public virtual Item Title { get; protected set; }

        public virtual Item Content { get; protected set; }

        public Page() { }

        public Page(string url, Theme theme, string title, string content)
        {
            this.Url = url;
            this.Theme = theme;
            this.Title = new Item("title", title, false);
            this.Content = new Item("content", content, true);
        }

        public virtual void Update(string url, Theme theme, string title, string content)
        {
            this.Url = url;
            this.Theme = theme;
            this.Title.Update(title);
            this.Content.Update(content);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Content
{
    public enum PageType
    {
        Base
    }

    public abstract class Page
    {
        public virtual int Id { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual string Content { get; protected set; }
        public virtual string Path { get; set; }
        public abstract PageType Type { get; }

        public Page()
        {
        }

        public Page(string title, string content, string path)
        {
            this.Title = title;
            this.Content = content;
            this.Path = path;
        }

        public virtual void Update(string title, string content, string path)
        {
            this.Title = title;
            this.Content = content;
            this.Path = path;
        }
    }

    public class BasePage : Page
    {
        public override PageType Type
        {
            get { return PageType.Base; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class ContentPage
    {
        public virtual int Id { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual string Content { get; protected set; }
        public virtual string Path { get; set; }

        public ContentPage()
        {
        }

        public ContentPage(string title, string content, string path)
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
}

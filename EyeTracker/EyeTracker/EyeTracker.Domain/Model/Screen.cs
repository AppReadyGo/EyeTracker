using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class Screen
    {
        public virtual int Id { get; set; }

        public virtual Application Application { get; set; }

        public virtual string Path { get; set; }

        public virtual int Width { get; set; }

        public virtual int Height { get; set; }

        public virtual string FileExtension { get; set; }

        public Screen()
        {
        }

        public Screen(Application application, string path, int width, int height, string fileExtention)
        {
            this.Application = application;
            this.Path = path;
            this.Width = width;
            this.Height = height;
            this.FileExtension = fileExtention;
        }

        public virtual void Update(string path, int width, int height, string fileExtention)
        {
            this.FileExtension = fileExtention;
            this.Height = height;
            this.Width = width;
            this.Path = path;
        }
    }
}

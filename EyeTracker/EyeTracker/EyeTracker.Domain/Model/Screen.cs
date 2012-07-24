using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class Screen
    {
        public virtual long Id { get; set; }

        public virtual Application Application { get; set; }

        public virtual string Path { get; set; }

        public virtual int Width { get; set; }

        public virtual int Height { get; set; }

        public virtual string FileExtension { get; set; }
    }
}

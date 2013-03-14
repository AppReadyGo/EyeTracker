using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class Package
    {
        public virtual int Id { get; protected set; }

        public virtual string FileName { get; protected set; }

        public virtual DateTime CreatedDate { get; protected set; }

        public Package()
        {
        }

        public Package(string fileName)
        {
            this.FileName = fileName;
            this.CreatedDate = DateTime.Now;
        }
    }
}

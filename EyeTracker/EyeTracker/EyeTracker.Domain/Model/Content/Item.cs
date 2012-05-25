using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Content
{
    public class Item
    {
        public virtual int Id { get; protected set; }
        public virtual string Key { get; protected set; }
        public virtual string SubKey { get; protected set; }
        public virtual string Value { get; protected set; }

        public Item()
        {
        }

        public Item(string key, string subKey, string value)
        {
            this.Key = key;
            this.SubKey = subKey;
            this.Value = value;
        }

        public virtual void Update(string value)
        {
            this.Value = value;
        }
    }
}

using System.Collections.Generic;

namespace EyeTracker.Domain.Model.Content
{
    public class Key
    {
        private Iesi.Collections.Generic.ISet<Item> items = null;

        public virtual int Id { get; protected set; }

        /// <summary>
        /// The property is readonly, can't changed programmatically just by script, the change involve code changes.
        /// </summary>
        public virtual string Url { get; protected set; }

        public virtual IEnumerable<Item> Items { get { return items; } }

        /// <summary>
        /// Key cannot be created programmatically just by script, the change involve code changes.
        /// </summary>

        public virtual void Update(Item[] items)
        {
            this.items.RetainAll(items);
            this.items.AddAll(items);
        }
    }
}

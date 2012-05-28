
namespace EyeTracker.Domain.Model.Content
{
    public class Item
    {
        public virtual int Id { get; protected set; }

        // TODO: future
        /*
        public virtual Language Language { get; protected set; }

        public virtual int Version { get; protected set; }
        */

        public virtual string SubKey { get; protected set; }

        public virtual bool IsHTML { get; protected set; }

        public virtual string Value { get; protected set; }

        public virtual Page Page { get; protected set; }

        public virtual SystemMail Mail { get; protected set; }

        public virtual Key Key { get; protected set; }

        public Item() { }

        public Item(string subKey, string value, bool isHTML)
        {
            this.SubKey = subKey;
            this.Value = value;
            this.IsHTML = isHTML;
        }

        public virtual void Update(string value)
        {
            this.Value = value;
        }

        public virtual void Update(string subKey, string value)
        {
            this.SubKey = subKey;
            this.Value = value;
        }
    }
}

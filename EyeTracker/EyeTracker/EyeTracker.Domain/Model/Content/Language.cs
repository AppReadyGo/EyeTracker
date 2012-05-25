
namespace EyeTracker.Domain.Model.Content
{
    public class Language
    {
        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string DisplayName { get; protected set; }

        public virtual string LocalName { get; protected set; }

        /// <summary>
        /// Language cannot be created programmatically just by script, the change involve code changes.
        /// </summary>
    }
}

using EyeTracker.Common;

namespace EyeTracker.Domain.Model.Content
{
    public class Theme
    {
        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        /// <summary>
        /// The property is readonly, can't changed programmatically just by script, the change involve code changes.
        /// </summary>
        public virtual string Url { get; protected set; }

        /// <summary>
        /// The property is readonly, can't changed programmatically just by script, the change involve code changes.
        /// </summary>
        public virtual ThemeType Type { get; protected set; }

        /// <summary>
        /// Theme cannot be created programmatically just by script, the change involve code changes.
        /// </summary>
    }
}

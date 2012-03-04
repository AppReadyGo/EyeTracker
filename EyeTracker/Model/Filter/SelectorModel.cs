using System.Collections.Generic;

namespace EyeTracker.Model
{
    public class SelectorModel
    {
        public string Title { get; set; }

        public IEnumerable<SelectorItem> Items { get; set; }

        public IEnumerable<SelectorItem> SelectedItems { get; set; }
    }

    public class SelectorItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
    }
}
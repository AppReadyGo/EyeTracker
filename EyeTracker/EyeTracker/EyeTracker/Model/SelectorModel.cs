using System.Collections.Generic;

namespace EyeTracker.Model
{
    public class SelectorModel
    {
        public string Title { get; set; }

        public Dictionary<int, string> Items { get; set; }

        public Dictionary<int, string> SelectedItems { get; set; }
    }
}
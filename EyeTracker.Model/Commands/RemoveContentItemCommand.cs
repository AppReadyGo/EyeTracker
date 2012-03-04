using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands
{
    public class RemoveContentItemCommand : ICommand
    {
        public string Key { get; protected set; }
        public string SubKey { get; protected set; }

        public RemoveContentItemCommand(string key, string subKey = null)
        {
            this.Key = key;
            this.SubKey = subKey;
        }

        public IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(this.Key))
            {
                yield return "Command must have Key parameter.";
            }
        }
    }
}

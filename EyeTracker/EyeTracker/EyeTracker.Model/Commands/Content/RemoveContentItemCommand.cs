using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands.Content
{
    public class RemoveContentItemCommand : ICommand<int>
    {
        public string Key { get; protected set; }
        public string SubKey { get; protected set; }

        public RemoveContentItemCommand(string key, string subKey = null)
        {
            this.Key = key;
            this.SubKey = subKey;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Key))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have Key parameter.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}

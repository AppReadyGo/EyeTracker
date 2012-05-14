using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands
{
    public class UpdateContentItemCommand : ICommand<bool>
    {
        public string Key { get; protected set; }
        public string SubKey { get; protected set; }
        public string Value { get; protected set; }

        public UpdateContentItemCommand(string key, string subKey, string value)
        {
            this.Key = key;
            this.SubKey = subKey;
            this.Value = value;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Key))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have Key parameter.");
            }

            if (string.IsNullOrEmpty(this.SubKey))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have SubKey parameter.");
            }

            if (string.IsNullOrEmpty(this.Value))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have Value parameter.");
            }

            if (this.Key.CheckLength(1, 50))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command parameter Value length have to be in range between 1 and 50 characters.");
            }

            if (this.SubKey.CheckLength(1, 50))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command parameter SubKey length have to be in range between 1 and 50 characters.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}

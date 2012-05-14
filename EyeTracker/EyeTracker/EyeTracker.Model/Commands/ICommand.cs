using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands
{
    public interface ICommand<TResult>
    {
        IEnumerable<ValidationResult> Validate(IValidationContext validation);
        IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security);
    }

    public class CommandResult<TResult>
    {
        public IEnumerable<ValidationResult> Validation { get; set; }
        public TResult Result { get; set; }
    }
}

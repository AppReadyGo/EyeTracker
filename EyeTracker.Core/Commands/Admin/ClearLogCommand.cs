using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Core.Commands.Admin
{
    public class ClearLogCommand : ICommand<bool>
    {
        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            yield break;
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}

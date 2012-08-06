using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands.Users
{
    public class UpdateLastAccessCommand : ICommand<bool>
    {
        public int UserId { get; private set; }

        public UpdateLastAccessCommand(int userId)
        {
            this.UserId = userId;
        }

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

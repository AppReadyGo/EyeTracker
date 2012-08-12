using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands.Users
{
    public class DeactivateUserCommand : ICommand<bool>
    {
        public int Id { get; set; }

        public DeactivateUserCommand(int id)
        {
            this.Id = id;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (Id == default (int))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "UserId is required parameter");
            }
            //not validating user data(email etc); should be done in user registration
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}

using System.Collections.Generic;

namespace EyeTracker.Common.Commands.Users
{

    public class RemoveUserCommand : ICommand<int>
    {
        public int Id { get; set; }

        public RemoveUserCommand(int id)
        {
            this.Id = id;
        }

        public virtual IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            //TODO: Add a checking that the user is admin
            yield break;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            yield break;
        }
    }
}

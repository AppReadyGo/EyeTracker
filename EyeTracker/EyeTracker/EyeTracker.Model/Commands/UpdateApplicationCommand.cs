using System.Collections.Generic;
using EyeTracker.Common.Entities;

namespace EyeTracker.Common.Commands
{
    public class UpdateApplicationCommand : ICommand<int>
    {
        public int Id { get; protected set; }
        public string Description { get; protected set; }

        public UpdateApplicationCommand(int id, string description)
        {
            this.Id = id;
            this.Description = description;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have Description parameter.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands.Users
{
    public class GrantSpecialAccessCommand : ICommand<bool>
    {
        public int Id { get; set; }
        public bool SpecialAccess { get; set; }

        public GrantSpecialAccessCommand(int id, bool specialAccess)
        {
            this.Id = id;
            this.SpecialAccess = specialAccess;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (Id == default(int))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "UserId is required parameter");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}

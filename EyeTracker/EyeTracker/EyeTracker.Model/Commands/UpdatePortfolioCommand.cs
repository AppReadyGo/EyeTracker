using System.Collections.Generic;
using EyeTracker.Common.Entities;

namespace EyeTracker.Common.Commands
{
    public class UpdatePortfolioCommand : ICommand<int>
    {
        public int Id { get; protected set; }
        public string Description { get; protected set; }
        public int TimeZone { get; protected set; }

        public UpdatePortfolioCommand(int id, string description, int timeZone)
        {
            this.Id = id;
            this.Description = description;
            this.TimeZone = timeZone;
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

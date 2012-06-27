using System.Collections.Generic;
using EyeTracker.Common.Entities;

namespace EyeTracker.Common.Commands
{
    public class CreatePortfolioCommand : ICommand<int>
    {
        public int PortfolioId { get; protected set; }
        public string Description { get; protected set; }
        public int TimeZone { get; protected set; }

        public CreatePortfolioCommand(string description, int timeZone)
        {
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

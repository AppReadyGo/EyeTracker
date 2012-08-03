using System.Collections.Generic;
using EyeTracker.Common.Entities;

namespace EyeTracker.Common.Commands.Application
{
    public class CreateApplicationCommand : ICommand<int>
    {
        public int PortfolioId { get; protected set; }
        public string Description { get; protected set; }
        public ApplicationType Type { get; protected set; }

        public CreateApplicationCommand(int portfolioId, string description, ApplicationType type)
        {
            this.PortfolioId = portfolioId;
            this.Description = description;
            this.Type = type;
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

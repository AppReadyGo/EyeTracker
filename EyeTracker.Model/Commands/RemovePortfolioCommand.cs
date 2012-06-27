using System.Collections.Generic;
using EyeTracker.Common.Entities;

namespace EyeTracker.Common.Commands
{
    public class RemovePortfolioCommand : ICommand<int>
    {
        public int Id { get; protected set; }

        public RemovePortfolioCommand(int id)
        {
            this.Id = id;
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

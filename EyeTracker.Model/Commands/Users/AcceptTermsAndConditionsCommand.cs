using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands.Users
{
    public class AcceptTermsAndConditionsCommand : ICommand<bool>
    {
        public int Id { get; set; }

        public bool Reset { get; set; }

        public AcceptTermsAndConditionsCommand(int id, bool reset = false)
        {
            this.Id = id;
            this.Reset = reset;
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

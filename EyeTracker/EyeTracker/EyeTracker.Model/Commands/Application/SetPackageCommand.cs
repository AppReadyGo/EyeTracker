using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands.Application
{
    public class SetPackageCommand : ICommand<int>
    {
        public int ApplicationId { get; private set; }

        public string FileName { get; private set; }

        public SetPackageCommand(int applicationId, string fileName)
        {
            this.ApplicationId = applicationId;
            this.FileName = fileName;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.FileName))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have FileName parameter.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands.Content
{
    public class UpdateThemeCommand : ICommand<int>
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public UpdateThemeCommand(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Name is required for the command");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            throw new NotImplementedException();
        }
    }
}

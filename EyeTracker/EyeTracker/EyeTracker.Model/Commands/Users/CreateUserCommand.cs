using System.Collections.Generic;

namespace EyeTracker.Common.Commands.Users
{

    public abstract class CreateUserCommand : ICommand<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        protected CreateUserCommand(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public virtual IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                yield return new ValidationResult(ErrorCode.WrongEmail, "The command must have an Email parameter.");
            }

            if (!validation.IsCorrectEmail(this.Email))
            {
                yield return new ValidationResult(ErrorCode.WrongEmail, "The email is wrong.");
            }

            if (validation.IsEmailExists(this.Email))
            {
                yield return new ValidationResult(ErrorCode.EmailExists, "The email exists in the system.");
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                yield return new ValidationResult(ErrorCode.WrongPassword, "The command must have an Password parameter.");
            }

            if (!validation.IsCorrectPassword(this.Password))
            {
                yield return new ValidationResult(ErrorCode.WrongPassword, "The password is wrong.");
            }
        }
    }

    public class CreateMemberCommand : CreateUserCommand
    {
        public CreateMemberCommand(string email, string password)
            : base(email, password)
        {
        }
    }

    public class CreateStaffCommand : CreateUserCommand
    {
        public CreateStaffCommand(string email, string password)
            : base(email, password)
        {
        }
    }
}

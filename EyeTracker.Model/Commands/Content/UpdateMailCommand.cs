using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands.Content
{
    public class UpdateMailCommand : ICommand<int>
    {
        public int Id { get; private set; }

        public string Url { get; private set; }

        public int ThemeId { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public UpdateMailCommand(int id, string url, int themeId, string subject, string body)
        {
            this.Id = id;
            this.Url = url;
            this.ThemeId = themeId;
            this.Subject = subject;
            this.Body = body;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Url))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Url is required for the command");
            }

            if (string.IsNullOrEmpty(this.Subject))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Subject is required for the command");
            }

            if (string.IsNullOrEmpty(this.Body))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Body is required for the command");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            throw new NotImplementedException();
        }
    }

    public class UpdateSystemMailCommand : ICommand<int>
    {
        public int Id { get; private set; }

        public int ThemeId { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public UpdateSystemMailCommand(int id, int themeId, string subject, string body)
        {
            this.Id = id;
            this.ThemeId = themeId;
            this.Subject = subject;
            this.Body = body;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {

            if (string.IsNullOrEmpty(this.Subject))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Subject is required for the command");
            }

            if (string.IsNullOrEmpty(this.Body))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Body is required for the command");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            throw new NotImplementedException();
        }
    }
}

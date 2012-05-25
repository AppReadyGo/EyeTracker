using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands.Content
{
    public class UpdatePageCommand : ICommand<int>
    {
        public virtual string Url { get; private set; }

        public virtual int ThemeId { get; private set; }

        public virtual string Title { get; private set; }

        public virtual string Content { get; private set; }

        public UpdatePageCommand(string url, int themeId, string title, string content)
        {
            this.Url = url;
            this.ThemeId = themeId;
            this.Title = title;
            this.Content = content;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Url))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Url is required for the command");
            }

            if (string.IsNullOrEmpty(this.Title))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Title is required for the command");
            }

            if (string.IsNullOrEmpty(this.Content))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Content is required for the command");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            throw new NotImplementedException();
        }
    }
}

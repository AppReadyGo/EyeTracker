using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeTracker.Common.Commands.Content
{
    public class UpdateKeyCommand : ICommand<int>
    {
        public int Id { get; private set; }

        public IEnumerable<Item> Items { get; private set; }

        public UpdateKeyCommand(int id, IEnumerable<Item> items)
        {
            this.Id = id;
            this.Items = items;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (this.Items.Any(i => string.IsNullOrEmpty(i.SubKey) || string.IsNullOrEmpty(i.Value)))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Parameter Items is wrong for the command");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            throw new NotImplementedException();
        }

        public class Item
        {
            public int Id { get; set; }
            public string SubKey { get; set; }
            public string Value { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common
{
    public interface IValidationContext
    {
        bool IsEmailExists(string email);

        bool IsCorrectEmail(string email);

        bool IsCorrectPassword(string password);

        bool IsExistsTag(string tag);
    }
}

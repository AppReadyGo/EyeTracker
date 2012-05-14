using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common
{
    public interface IValidationContext
    {
        bool IsExistsEmail(string email);

        bool IsCorrectEmail(string email);

        bool IsCorrectPassword(string password);

        bool IsExistsTag(string tag);
    }

    public class ValidationContext : IValidationContext
    {
        public bool IsExistsEmail(string email)
        {
            return false;
        }

        public bool IsCorrectEmail(string email)
        {
            return true;
        }

        public bool IsCorrectPassword(string password)
        {
            return true;
        }

        public bool IsExistsTag(string tag)
        {
            return false;
        }
    }
}

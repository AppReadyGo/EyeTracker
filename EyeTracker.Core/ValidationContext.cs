using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Domain.Model.Users;

namespace EyeTracker.Core
{
    public class ValidationContext : IValidationContext
    {
        private ISession session = null;

        public ValidationContext(ISession session)
        {
            this.session = session;
        }
        public bool IsEmailExists(string email)
        {
            return this.session.Query<User>()
                            .Where(u => u.Email.ToLower() == email.ToLower())
                            .Any();
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

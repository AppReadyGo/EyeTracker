using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Commands.Users;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Domain.Model.Users;

namespace EyeTracker.Domain.CommandHandlers.Users
{
    public class AcceptTermsAndConditionsCommandHandler : ICommandHandler<AcceptTermsAndConditionsCommand, bool>
    {
        public bool Execute(ISession session, AcceptTermsAndConditionsCommand cmd)
        {
            var user = session.Query<User>()
                            .Where(u => u.Id == cmd.Id)
                            .Select(u => u)
                            .SingleOrDefault();
            if (user != null)
            {
                user.AcceptTermsAndConditions(cmd.Reset);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

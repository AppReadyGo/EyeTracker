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
    public class UnsubscribeCommandHandler : ICommandHandler<UnsubscribeCommand, bool>
    {
        public bool Execute(ISession session, UnsubscribeCommand cmd)
        {
            var user = session.Query<User>()
                            .Where(u => u.Email.ToLower() == cmd.Email.ToLower())
                            .Select(u => u)
                            .SingleOrDefault();
            if (user != null)
            {
                user.Unsubscribe();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

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
    public class UpdateLastAccessCommandHandler : ICommandHandler<UpdateLastAccessCommand, bool>
    {
        public bool Execute(ISession session, UpdateLastAccessCommand cmd)
        {
            var user = session.Query<User>()
                            .Where(u => u.Id == cmd.UserId)
                            .Select(u => u)
                            .SingleOrDefault();
            if (user != null)
            {
                user.UpdateLastAccess();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

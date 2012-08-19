using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Commands.Users;
using EyeTracker.Domain.Model.Users;
using NHibernate;

namespace EyeTracker.Domain.CommandHandlers.Users
{
    public class GrantSpecialAccessCommandHandler : ICommandHandler<GrantSpecialAccessCommand, bool>
    {
        public bool Execute(ISession session, GrantSpecialAccessCommand cmd)
        {
            try
            {
                var user = session.Get<User>(cmd.Id);
                if (user != null)
                {
                    user.GrantSpecialAccess(cmd.SpecialAccess);
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
            return false;
        }
    }
}

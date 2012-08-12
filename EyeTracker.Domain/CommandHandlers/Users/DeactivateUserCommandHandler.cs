using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Commands.Users;
using EyeTracker.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.CommandHandlers.Users
{
    public class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand, bool>
    {
        public bool Execute(ISession session, DeactivateUserCommand cmd)
        {
            try
            {
                var user = session.Get<User>(cmd.Id);
                if (user != null)
                {
                    user.Deactivate();
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

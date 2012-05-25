using System.Linq;
using EyeTracker.Common.Commands.Users;
using EyeTracker.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;

namespace EyeTracker.Domain.CommandHandlers.Users
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, bool>
    {
        public bool Execute(ISession session, ResetPasswordCommand cmd)
        {
            var user = session.Query<User>()
                            .Where(u => u.Email.ToLower() == cmd.Email.ToLower())
                            .Select(u => u)
                            .SingleOrDefault();
            if (user != null)
            {
                user.ChangePassword(cmd.Password);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

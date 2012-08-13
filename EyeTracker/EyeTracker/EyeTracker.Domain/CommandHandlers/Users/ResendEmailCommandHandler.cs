using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Commands.Users;
using EyeTracker.Common;
using EyeTracker.Domain.Model.Users;
using NHibernate;

namespace EyeTracker.Domain.CommandHandlers.Users
{
    public class ResendEmailCommandHandler : ICommandHandler<ResendEmailCommand, bool>
    {

        public bool Execute(ISession session, ResendEmailCommand cmd)
        {
            try
            {
                var user = session.Get<User>(cmd.Id);
                if (user != null)
                {
                    //todo: use this from AdminController and AccountController
                    //new MailGenerator(this.ControllerContext).Send(new ActivationEmail(user.Email));
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using EyeTracker.Common.Commands.Users;
using EyeTracker.Domain.Model.Users;

namespace EyeTracker.Domain.CommandHandlers.Users
{
    public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, int>
    {
        public int Execute(ISession session, CreateMemberCommand cmd)
        {
            var user = new User(cmd.Email, cmd.Password);
            session.Save(user);
            return user.Id;
        }
    }

    public class CreateStaffCommandHandler : ICommandHandler<CreateStaffCommand, int>
    {
        public int Execute(ISession session, CreateStaffCommand cmd)
        {
            var user = new Staff(cmd.Email, cmd.Password);
            session.Save(user);
            return user.Id;
        }
    }
}

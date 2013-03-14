using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Common.Commands.Application;
using EyeTracker.Common;
using EyeTracker.Domain.Model.Users;

namespace EyeTracker.Domain.CommandHandlers.Application
{
    public class CreateApplicationCommandHandler : ICommandHandler<CreateApplicationCommand, int>
    {
        private ISecurityContext securityContext;

        public CreateApplicationCommandHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public int Execute(ISession session, CreateApplicationCommand cmd)
        {
            var user = session.Get<User>(securityContext.CurrentUser.Id);
            var app = new Model.Application(user, cmd.Description, cmd.Type);
            session.Save(app);
            return app.Id;
        }
    }
}

using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;

namespace EyeTracker.Domain.CommandHandlers
{
    public class UpdateApplicationCommandHandler : ICommandHandler<UpdateApplicationCommand, int>
    {
        public int Execute(ISession session, UpdateApplicationCommand cmd)
        {
            var app = session.Get<Application>(cmd.Id);
            app.Update(cmd.Description);
            return app.Id;
        }
    }
}

using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Common.Commands.Application;

namespace EyeTracker.Domain.CommandHandlers.Application
{
    public class UpdateApplicationCommandHandler : ICommandHandler<UpdateApplicationCommand, int>
    {
        public int Execute(ISession session, UpdateApplicationCommand cmd)
        {
            var app = session.Get<Model.Application>(cmd.Id);
            app.Update(cmd.Description);
            return app.Id;
        }
    }
}

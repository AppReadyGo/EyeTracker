using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Common.Commands.Application;

namespace EyeTracker.Domain.CommandHandlers.Application
{
    public class RemoveApplicationCommandHandler : ICommandHandler<RemoveApplicationCommand, int>
    {
        public int Execute(ISession session, RemoveApplicationCommand cmd)
        {
            var app = session.Get<Model.Application>(cmd.Id);
            session.Delete(app);
            return app.Id;
        }
    }
}

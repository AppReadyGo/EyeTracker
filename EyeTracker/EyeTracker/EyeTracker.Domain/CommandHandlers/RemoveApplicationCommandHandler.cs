using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;

namespace EyeTracker.Domain.CommandHandlers
{
    public class RemoveApplicationCommandHandler : ICommandHandler<RemoveApplicationCommand, int>
    {
        public int Execute(ISession session, RemoveApplicationCommand cmd)
        {
            var app = session.Get<Application>(cmd.Id);
            session.Delete(app);
            return app.Id;
        }
    }
}

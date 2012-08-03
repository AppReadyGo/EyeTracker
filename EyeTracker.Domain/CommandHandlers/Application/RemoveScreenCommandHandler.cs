using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Common.Commands.Application;

namespace EyeTracker.Domain.CommandHandlers.Application
{
    public class RemoveScreenCommandHandler : ICommandHandler<RemoveScreenCommand, int>
    {
        public int Execute(ISession session, RemoveScreenCommand cmd)
        {
            var screen = session.Get<Model.Screen>(cmd.Id);
            screen.Application.RemoveScreen(screen);
            session.Delete(screen);
            return screen.Id;
        }
    }
}

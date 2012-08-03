using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Common.Commands.Application;

namespace EyeTracker.Domain.CommandHandlers.Application
{
    public class AddScreenCommandHandler : ICommandHandler<AddScreenCommand, long>
    {
        public long Execute(ISession session, AddScreenCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var screen = new Model.Screen(application, cmd.Path, cmd.Width, cmd.Height, cmd.FileExtention);
            application.AddScreen(screen);
            session.Save(screen);
            return screen.Id;
        }
    }
}

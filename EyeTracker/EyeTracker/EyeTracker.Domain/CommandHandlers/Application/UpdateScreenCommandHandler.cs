using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Common.Commands.Application;

namespace EyeTracker.Domain.CommandHandlers.Application
{
    public class UpdateScreenCommandHandler : ICommandHandler<UpdateScreenCommand, int>
    {
        public int Execute(ISession session, UpdateScreenCommand cmd)
        {
            var screen = session.Get<Model.Screen>(cmd.Id);
            screen.Update(cmd.Path, cmd.Width, cmd.Height, cmd.FileExtention);
            session.Update(screen);
            return screen.Id;
        }
    }
}

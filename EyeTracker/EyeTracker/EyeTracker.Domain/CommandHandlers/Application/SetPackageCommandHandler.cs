using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Common.Commands.Application;

namespace EyeTracker.Domain.CommandHandlers.Application
{
    public class SetPackageCommandHandler : ICommandHandler<SetPackageCommand, int>
    {
        public int Execute(ISession session, SetPackageCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var pkg = new Package(cmd.FileName);
            application.SetPakage(pkg);
            session.Save(pkg);
            return pkg.Id;
        }
    }
}

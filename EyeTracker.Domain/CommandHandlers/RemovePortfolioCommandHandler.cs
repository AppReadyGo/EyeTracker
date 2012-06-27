using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;

namespace EyeTracker.Domain.CommandHandlers
{
    public class RemovePortfolioCommandHandler : ICommandHandler<RemovePortfolioCommand, int>
    {
        public int Execute(ISession session, RemovePortfolioCommand cmd)
        {
            var obj = session.Get<Portfolio>(cmd.Id);
            session.Delete(obj);
            return obj.Id;
        }
    }
}

using NHibernate;

namespace EyeTracker.Domain.CommandHandlers
{
    public interface ICommandHandler<TCommand, TResult>
    {
        TResult Execute(ISession session, TCommand cmd);
    }
}

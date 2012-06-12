using EyeTracker.Common.Commands;
using EyeTracker.Common.Queries;

namespace EyeTracker.Common
{
    public interface IObjectContainer
    {
        TResult RunQuery<TResult>(IQuery<TResult> query);
        CommandResult<TResult> Dispatch<TResult>(ICommand<TResult> command);
        CurrentUserDetails CurrentUserDetails { get; }
    }
}

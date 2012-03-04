using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.CommandHandlers
{
    public interface ICommandHandler<TCommand>
    {
        void Execute(TCommand cmd);
    }
}

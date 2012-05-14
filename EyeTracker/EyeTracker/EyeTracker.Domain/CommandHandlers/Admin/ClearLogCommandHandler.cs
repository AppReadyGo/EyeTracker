using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using EyeTracker.Common.Commands.Admin;

namespace EyeTracker.Domain.CommandHandlers.Admin
{
    public class ClearLogCommandHandler : StoredProcedureCommandHandler, ICommandHandler<ClearLogCommand, bool>
    {
        public bool Execute(ISession session, ClearLogCommand cmd)
        {
            ExecuteStoredProcedure(session, cmd, "log");

            return true;
        }
    }
}

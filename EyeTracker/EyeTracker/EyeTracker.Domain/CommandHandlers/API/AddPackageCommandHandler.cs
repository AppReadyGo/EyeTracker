using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Commands.API;
using NHibernate;
using EyeTracker.Domain.Model;

namespace EyeTracker.Domain.CommandHandlers.API
{
    public class AddPackageCommandHandler : ICommandHandler<AddPackageCommand, int>
    {
        public int Execute(ISession session, AddPackageCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);

            foreach (var s in cmd.Sessions)
            {
                /*
                var pageView = new PageView(application, s.StartDate, cmd);
                application.AddPageView(pageView);
                 * */
            }
            return 1;
        }
    }
}

﻿using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Common.Commands.Application;

namespace EyeTracker.Domain.CommandHandlers.Application
{
    public class CreateApplicationCommandHandler : ICommandHandler<CreateApplicationCommand, int>
    {
        public int Execute(ISession session, CreateApplicationCommand cmd)
        {
            var portfolio = session.Get<Portfolio>(cmd.PortfolioId);
            var app = new Model.Application(portfolio, cmd.Description, cmd.Type);
            session.Save(app);
            return app.Id;
        }
    }
}
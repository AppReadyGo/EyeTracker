using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using EyeTracker.Common.Commands;

namespace EyeTracker.Domain.CommandHandlers
{
    public class CreateContentItemCommandHandler : ICommandHandler<CreateContentItemCommand>
    {
        private IRepository repository;

        public CreateContentItemCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }   

        public void Execute(CreateContentItemCommand cmd)
        {
            var item = new ContentItem(cmd.Key, cmd.SubKey, cmd.Value);
            this.repository.Add(item);
        }

    }
}

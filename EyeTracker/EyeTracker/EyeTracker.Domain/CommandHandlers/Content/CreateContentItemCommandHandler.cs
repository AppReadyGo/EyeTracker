using EyeTracker.Common.Commands;
using EyeTracker.Domain.Model.Content;
using NHibernate;

namespace EyeTracker.Domain.CommandHandlers.Content
{
    //public class CreateContentItemCommandHandler : ICommandHandler<CreateContentItemCommand, int>
    //{
    //    private IRepository repository;

    //    public CreateContentItemCommandHandler(IRepository repository)
    //    {
    //        this.repository = repository;
    //    }   

    //    public int Execute(ISession session, CreateContentItemCommand cmd)
    //    {
    //        var item = new Item(cmd.Key, cmd.SubKey, cmd.Value);
    //        this.repository.Add(item);
    //        return item.Id;
    //    }
    //}
}

//using EyeTracker.Common.Commands;
//using EyeTracker.Domain.Model;
//using NHibernate;
//using EyeTracker.Domain.Model.Users;
//using EyeTracker.Common;

//namespace EyeTracker.Domain.CommandHandlers
//{
//    public class CreatePortfolioCommandHandler : ICommandHandler<CreatePortfolioCommand, int>
//    {
//        private ISecurityContext securityContext;

//        public CreatePortfolioCommandHandler(ISecurityContext securityContext)
//        {
//            this.securityContext = securityContext;
//        }

//        public int Execute(ISession session, CreatePortfolioCommand cmd)
//        {
//            var user = session.Get<User>(securityContext.CurrentUser.Id);
//            var portfolio = new Portfolio(cmd.Description, cmd.TimeZone, user);
//            session.Save(portfolio);
//            return portfolio.Id;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using EyeTracker.Common.Queries;
using EyeTracker.Domain.Queries;
using EyeTracker.Domain.CommandHandlers;
using System.Reflection;
using EyeTracker.Common.Commands;
using EyeTracker.Domain;
using NHibernate;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using System.Configuration;
using NHibernate.Mapping.ByCode;
using NHibernate.Driver;
using NHibernate.Dialect;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using EyeTracker.Core.Services;
using EyeTracker.Common;

namespace EyeTracker.Core
{
    public interface IObjectContainer
    {
        TResult RunQuery<TResult>(IQuery<TResult> query);
        CommandResult<TResult> Dispatch<TResult>(ICommand<TResult> command);
    }

    public class ObjectContainer : IObjectContainer
    {
        private static readonly object locker = new object();
        private static IObjectContainer instance = null;

        private readonly WindsorContainer container = new WindsorContainer();
        private IRepository repository;

        private ISessionFactory sessionFactory;
        private NHibernate.Cfg.Configuration configuration;

        public static IObjectContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ObjectContainer();
                        }
                    }
                }
                return instance;
            }
        }

        private ObjectContainer()
        {
            var dbSettings = (DatabaseSettings)ConfigurationManager.GetSection("dataConfiguration");
            this.sessionFactory = BuildSessionFactory(typeof(NHibernateHelper), ConfigurationManager.ConnectionStrings[dbSettings.DefaultDatabase].ToString());

            var parentCommandHandler = typeof(ICommandHandler<,>);
            foreach (var type in parentCommandHandler.Assembly.GetTypes())
            {
                if (type.IsClass)
                {
                    var @interface = type.GetInterfaces().FirstOrDefault();
                    if (@interface != null && @interface.IsGenericType && @interface.GetGenericTypeDefinition() == parentCommandHandler)
                    {
                        container.Register(Component.For(@interface).ImplementedBy(type));
                    }
                }
            }

            var parentQueryHandler = typeof(IQueryHandler<,>);
            foreach (var type in parentQueryHandler.Assembly.GetTypes())
            {
                if (type.IsClass)
                {
                    var @interface = type.GetInterfaces().FirstOrDefault();
                    if (@interface != null && @interface.IsGenericType && @interface.GetGenericTypeDefinition() == parentQueryHandler)
                    {
                        container.Register(Component.For(@interface).ImplementedBy(type));
                    }
                }
            }
            container.Register(Component.For<IRepository>().ImplementedBy<Repository>());
            container.Register(Component.For<ISecurityContext>().ImplementedBy<SecurityContext>());
            container.Register(Component.For<IValidationContext>().ImplementedBy<ValidationContext>());

            this.repository = container.Resolve<IRepository>();
        }

        private ICommandHandler<TCommand, TResult> GetCommandHandler<TCommand, TResult>(ICommand<TResult> command)
        {
            return container.Resolve<ICommandHandler<TCommand, TResult>>();
        }

        private ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }

        private ISessionFactory BuildSessionFactory(Type type, string connectionString)
        {
            var mapper = new ModelMapper();

            mapper.AddMappings(type.Assembly.GetTypes());

            //mapper.AddMappings(typeof(OrganisationMapping).Assembly.GetTypes());

            var cfg = new NHibernate.Cfg.Configuration();

            cfg.DataBaseIntegration(c =>
            {
                c.ConnectionString = connectionString;
                c.Driver<SqlClientDriver>();
                c.Dialect<MsSql2008Dialect>();

#if DEBUG
                c.LogSqlInConsole = true;
                c.LogFormattedSql = true;
                //c.SchemaAction = SchemaAutoAction.Create;
                c.SchemaAction = SchemaAutoAction.Validate;
#else
                c.SchemaAction = SchemaAutoAction.Validate;
#endif
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;

            });

            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            SchemaMetadataUpdater.QuoteTableAndColumns(cfg);

            //var cmpl = mapper.CompileMappingFor(new List<Type>() { typeof(Application), typeof(Role), typeof(User) });
            //var cmpl = mapper.CompileMappingFor(new List<Type>() { typeof(Address), typeof(Organisation), typeof(OrganisationLicences), typeof(OrganisationJoined), typeof(Country), typeof(OrganisationType) });
            //var str = Serialize(cmpl);
            //Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(str));

            return cfg.BuildSessionFactory();
        }

        public TResult RunQuery<TResult>(IQuery<TResult> query)
        {
            Type handlerTypeBluprint = typeof(IQueryHandler<,>);
            Type[] typeArgs = { query.GetType(), typeof(TResult) };
            var obj = container.Resolve(handlerTypeBluprint.MakeGenericType(typeArgs));

            MethodInfo method = obj.GetType().GetMethod("Run");

            using (ISession session = OpenSession())
            {

                return (TResult)method.Invoke(obj, new object[] { session, query });
            }
        }

        public CommandResult<TResult> Dispatch<TResult>(ICommand<TResult> command)
        {
            Type handlerTypeBluprint = typeof(ICommandHandler<,>);
            Type[] typeArgs = { command.GetType(), typeof(TResult) };
            var obj = container.Resolve(handlerTypeBluprint.MakeGenericType(typeArgs));
            MethodInfo method = obj.GetType().GetMethod("Execute");
            var commandResult = new CommandResult<TResult>();
            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    var list = new List<ValidationResult>();
                    list.AddRange(command.ValidatePermissions(container.Resolve<ISecurityContext>()));
                    list.AddRange(command.Validate(new ValidationContext(session)));
                    commandResult.Validation = list;
                    if (!list.Any())
                    {
                        commandResult.Result = (TResult)method.Invoke(obj, new object[] { session, command });
                        dbTrans.Commit();
                    }
                    else
                    {
                        dbTrans.Rollback();
                    }
                }
                return commandResult;
            }
            //TODO: Events
        }
    }
}

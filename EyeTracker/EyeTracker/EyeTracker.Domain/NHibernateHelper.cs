using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Driver;
using NHibernate.Dialect;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using NHibernate.Tool.hbm2ddl;

namespace EyeTracker.Domain
{
    public sealed class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static NHibernate.Cfg.Configuration _configuration;

        public static string NHibernateSQL { get; set; }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    DatabaseSettings dbSettings = (DatabaseSettings)ConfigurationManager.GetSection("dataConfiguration");
                    _sessionFactory = BuildSessionFactory(typeof(NHibernateHelper), ConfigurationManager.ConnectionStrings[dbSettings.DefaultDatabase].ToString());
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        private static ISessionFactory BuildSessionFactory(Type type, string connectionString)
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


    }    

    public class NHSQLInterceptor : EmptyInterceptor, IInterceptor
    {
        NHibernate.SqlCommand.SqlString
            IInterceptor.OnPrepareStatement
                (NHibernate.SqlCommand.SqlString sql)
        {
            NHibernateHelper.NHibernateSQL = sql.ToString();
            return sql;
        }
    }
}

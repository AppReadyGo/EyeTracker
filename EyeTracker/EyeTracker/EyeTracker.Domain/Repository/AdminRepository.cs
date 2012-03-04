using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Cfg.MappingSchema;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Dialect;
using System.Configuration;
using EyeTracker.Domain.Model;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Domain.Repositories
{
    public interface IAdminRepository
    {
        MembershipInfo GetMembership();

        IList<T> GetAll<T>() where T : Entity, new();

        T Get<T>(Guid id) where T : Entity, new();

        void Edit<T>(T entity) where T : Entity, new();
    }

    public class AdminRepository : IAdminRepository
    {
        //public AdminRepository()
        //{
        //    if (sessionFactory == null)
        //        sessionFactory = BuildSessionFactory(this.GetType(), ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
        //}

        protected static string Serialize(HbmMapping hbmElement)
        {
            var setting = new XmlWriterSettings { Indent = true };
            var serializer = new XmlSerializer(typeof(HbmMapping));
            using (var memStream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(memStream, setting))
                {
                    serializer.Serialize(xmlWriter, hbmElement);
                    memStream.Flush();
                    byte[] streamContents = memStream.ToArray();

                    string result = Encoding.UTF8.GetString(streamContents);
                    return result;
                }
            }
        }

        public MembershipInfo GetMembership()
        {
            var res = new MembershipInfo();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                res.Applications = session.CreateCriteria<SystemApplication>().List<SystemApplication>();
                res.Roles = session.CreateCriteria<SystemRole>().List<SystemRole>();
                res.Users = session.CreateCriteria<SystemUser>().List<SystemUser>();
            }
            return res;
        }

        public IList<T> GetAll<T>() where T : Entity, new()
        {
            IList<T> res = null;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                res = session.QueryOver<T>().List();
            }
            return res;
        }

        public T Get<T>(Guid id) where T : Entity, new()
        {
            T res = null;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                res = session.Get<T>(id);
                var type = typeof(T);
                if (type == typeof(SystemUser))
                {
                    var tRes = res as SystemUser;
                    NHibernateUtil.Initialize(tRes.Roles);
                }
            }
            return res;
        }

        public void Edit<T>(T entity) where T : Entity, new()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(entity);
                    transaction.Commit();
                }
            }
        }

//        private static ISessionFactory BuildSessionFactory(Type type, string connectionString)
//        {
//            var mapper = new ModelMapper();

//            mapper.AddMappings(type.Assembly.GetTypes());

//            var cfg = new NHibernate.Cfg.Configuration();

//            cfg.DataBaseIntegration(c =>
//            {
//                c.ConnectionString = connectionString;
//                c.Driver<SqlClientDriver>();
//                c.Dialect<MsSql2008Dialect>();

//#if DEBUG
//                c.LogSqlInConsole = true;
//                c.LogFormattedSql = true;
//#endif
//                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
//                c.SchemaAction = SchemaAutoAction.Update;
//            });

//            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

//            //SchemaMetadataUpdater.QuoteTableAndColumns(cfg);

//            var cmpl = mapper.CompileMappingFor(new List<Type>() { typeof(SystemApplication), typeof(SystemRole), typeof(SystemUser) });
//            var str = Serialize(cmpl);
//            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(str));

//            return cfg.BuildSessionFactory();
//        }


    }

}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Switchie.Entity;

namespace Switchie.Data.Init
{
    class Program
    {
        static void Main(string[] args)
        {
            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Feature feat = new Feature { Name = "featuretest", Percentage = 50, IsEnabled = true, LastModifiedDate = DateTime.Now};
                    FeatureGroup grp = new FeatureGroup {Feature = feat, Name = "group1"};
                    FeatureUser usr = new FeatureUser {Feature = feat, Name = "user1"};
                    session.SaveOrUpdate(feat);
                    session.SaveOrUpdate(grp);
                    session.SaveOrUpdate(usr);
                    Feature globFeat = new Feature { Name = "globalfeat", Percentage = 100, IsEnabled = true, LastModifiedDate = DateTime.Now };
                    session.SaveOrUpdate(globFeat);
                    transaction.Commit();
                }
            }

            Console.Read();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("FeatureDB")).ShowSql()
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<Entity.FeatureMap>()
                .AddFromAssemblyOf<Entity.FeatureGroupMap>()
                .AddFromAssemblyOf<Entity.FeatureUserMap>())
                .ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            new SchemaExport(config)
                .Create(true, true);
        }
    }
}

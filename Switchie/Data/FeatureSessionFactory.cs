using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Switchie.Data
{
    public class FeatureSessionFactory
    {
        private static ISessionFactory _SessionFactory;

        public ISessionFactory GetSessionFactory()
        {
            return _SessionFactory ?? (_SessionFactory = Fluently.Configure()
                                                                 .Database(
                                                                     MsSqlConfiguration.MsSql2008.ConnectionString(
                                                                         c => c.FromConnectionStringWithKey("FeatureDB")).ShowSql())
                                                                 .Mappings(m =>
                                                                           m.FluentMappings
                                                                            .AddFromAssemblyOf<Entity.FeatureMap>()
                                                                            .AddFromAssemblyOf<Entity.FeatureGroupMap>()
                                                                            .AddFromAssemblyOf<Entity.FeatureUserMap>())
                                                                 .BuildSessionFactory());
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            new SchemaExport(config)
                .Create(true, false);
        }
    }
}

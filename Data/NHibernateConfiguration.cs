using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using TLCOnline.Data.Mappings;
using TLCOnline.Resources;

namespace TLCOnline.Data
{
    public class NHibernateConfiguration
    {
        public static ISessionFactory CreateSessionFactory()
        {

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(c => c
                        .Server("(localdb)\\MSSQLLocalDB")
                        .Database(Labels.TLCOnlineDB)
                        .TrustedConnection()))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<CustomerMap>())
                .BuildSessionFactory();
        }
    }
}
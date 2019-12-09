using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using TimeTracker.Core.Models;
using TimeTracker.Core.Utils;

namespace TimeTracker.Core.Data
{
    public class NHibernateHelper : INHibernateHelper
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static ISessionFactory _sessionFactory;
        static NHibernateHelper()
        {
            //_sessionFactory = FluentConfigure();
        }
        public static ISession GetCurrentSession()
        {
            return _sessionFactory.OpenSession();
        }
        public static void CloseSession()
        {
            _sessionFactory.Close();
        }
        public static void CloseSessionFactory()
        {
            if (_sessionFactory != null)
            {
                _sessionFactory.Close();
            }
        }

        public static ISessionFactory FluentConfigure(string cnStr)
        {

            Action<NHibernate.Cfg.Configuration> shema = TreatConfiguration;

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(cnStr).ShowSql())
                .Mappings(m => m.AutoMappings.Add(CreateMappings()))
                .ExposeConfiguration(shema)
                .Cache(
                    c => c.UseQueryCache()
                        .UseSecondLevelCache()
                        .ProviderClass<NHibernate.Cache.HashtableCacheProvider>())
                .BuildSessionFactory();
        }

        public static void TreatConfiguration(NHibernate.Cfg.Configuration configuration)
        {
            var update = new SchemaUpdate(configuration);
            update.Execute(false, true);
        }

        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap.Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(t => t.Namespace == "TimeTracker.Core.Models")
                .IgnoreBase<ABaseItem>()
                .UseOverridesFromAssembly(System.Reflection.Assembly.GetCallingAssembly())
                .Conventions
                .Setup(c => c.Add(DefaultCascade.SaveUpdate()));
        }


        public NHibernateHelper(IOptions<ApplicationSettings> config)
        {
            _sessionFactory = FluentConfigure(config.Value.ConnectionString);
        }
        public ISession getSession()
        {
            return GetCurrentSession();
        }
    }
}
using Autofac;
using Autofac.Integration.WebApi;
using PlaneSpotter.Repo;
using PlaneSpotter.Repo.Repo;
using PlaneSpotter.Services;
using System.Reflection;
using System.Web.Http;

namespace PlaneSpotter.Web.API.Dependancies
{
    public class WebapiConfigOverrideAutoFac
    {
        public static IContainer afContainer;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterApiControllers(assembly);
            builder.RegisterType<PlaneSpotterContext>().As<PlaneSpotterContext>().InstancePerLifetimeScope();

            builder.RegisterType<PlaneSpotterService>().As<IPlaneSpotterService>().InstancePerLifetimeScope();
            //ServiceConfig.RegisterServices(builder);
            builder.RegisterType<UnitofWork>().As<IUnitofWork>().InstancePerLifetimeScope();

            afContainer = builder.Build();
            return afContainer;
        }


    }
}
using Autofac;
using Autofac.Integration.Mvc;
using PlaneSpotter.Repo;
using PlaneSpotter.Repo.Repo;
using PlaneSpotter.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PlaneSpotter.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterType<PlaneSpotterService>().As<IPlaneSpotterService>().InstancePerLifetimeScope();
            //builder.RegisterType<PlaneSpotterService>().As<IPlaneSpotterService>().InstancePerLifetimeScope();

            builder.RegisterType<PlaneSpotterContext>().As<PlaneSpotterContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitofWork>().As<IUnitofWork>().InstancePerLifetimeScope();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}

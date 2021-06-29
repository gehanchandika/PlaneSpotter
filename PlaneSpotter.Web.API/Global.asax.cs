using Autofac;
using Autofac.Integration.Mvc;
using PlaneSpotter.Repo;
using PlaneSpotter.Repo.Repo;
using PlaneSpotter.Services;
using PlaneSpotter.Web.API.Dependancies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PlaneSpotter.Web.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            WebapiConfigOverrideAutoFac.Initialize(GlobalConfiguration.Configuration);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HttpConfiguration config = GlobalConfiguration.Configuration;

        }
    }
}

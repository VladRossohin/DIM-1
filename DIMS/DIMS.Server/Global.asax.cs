using DIMS.BL.Infrastructure;
using DIMS.Server.App_Start;
using DIMS.Server.utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.WebApi.Filter;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DIMS.Server
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // AutomapperConfig.Initialize();

            // Remove data annotations validation provider 
            ModelValidatorProviders.Providers.Remove(
                ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().First());

            DependencyInjection();
        }

        private void DependencyInjection()
        {
            // dependency injection
            var dependencesModule = new DependencesModule();

            var autoMapperServerModule = new AutoMapperModule();

            var serviceModule = new ServicesModule("DIMSDBConnection", "DIMSIdentityConnection");

            var kernel = new StandardKernel(dependencesModule, serviceModule, autoMapperServerModule);

            kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetModelValidatorProviders()));

            kernel.Bind<DefaultFilterProviders>().ToConstant(new DefaultFilterProviders(GlobalConfiguration.Configuration.Services.GetFilterProviders()));

            var ninjectResolver = new NinjectDependencyResolver(kernel);

            DependencyResolver.SetResolver(ninjectResolver);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var userObject = new UserObject
            {
                IsAdmin = User.IsInRole("admin")
            };

            HttpContext.Current.Session.Add("__userObject", userObject);
        }
    }
}

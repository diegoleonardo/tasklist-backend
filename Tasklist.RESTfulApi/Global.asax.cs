using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using Tasklist.Infra.DependencyInjection;

namespace Tasklist.RESTfulApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var container = DependencyLoader.LoadDependency();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}

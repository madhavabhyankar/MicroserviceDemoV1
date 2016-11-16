using System.Web.Http;
using Newtonsoft.Json;
using Owin;
using WindsorWebApiDependency;

namespace OrderService.Client
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.DependencyResolver = new WindsorDependencyResolver(OrderServiceRunnable.Container);
            app.UseWebApi(httpConfiguration);
        }
    }
}
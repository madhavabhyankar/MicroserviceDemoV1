using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OrderService.Client.Database;

namespace OrderService.Client
{
    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IOrderServiceDbContext>().ImplementedBy<OrderServiceDbContext>().LifestyleTransient(),
                Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleTransient()
            );
        }
    }
}

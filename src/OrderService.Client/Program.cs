﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin.Hosting;
using NServiceBus;
using Topshelf;

namespace OrderService.Client
{
    class Program
    {
       
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<OrderServiceRunnable>(s =>
                {
                    s.ConstructUsing(name => new OrderServiceRunnable());
                    s.WhenStarted(t => t.Start());
                    s.WhenStopped(t => t.Stop());

                });
                x.SetDisplayName("Order Service Client");
                x.RunAsLocalSystem();
            });
            
            

        }
    }

    public class OrderServiceRunnable
    {
        public static IWindsorContainer Container { get; set; } = new WindsorContainer();
        private IEndpointInstance endpointInstance;
        private IDisposable httpServer;
        public OrderServiceRunnable()
        {
            Container.Install(FromAssembly.This());
        }

        public void Start()
        {
            httpServer = WebApp.Start<OwinStartup>(url: "http://+:30060");
            StartNSB().GetAwaiter().GetResult();
        }

        public void Stop()
        {
            httpServer?.Dispose();
            endpointInstance?.Stop().ConfigureAwait(false);
            Container.Dispose();
            
        }

        async Task StartNSB()
        {
            var endpointConfiguration = new EndpointConfiguration("OrderService.Client");
            endpointConfiguration.SendFailedMessagesTo("error");

            // Use JSON to serialize and deserialize messages (which are just
            // plain classes) to and from message queues
            endpointConfiguration.UseSerialization<JsonSerializer>();

            // Ask NServiceBus to automatically create message queues
            endpointConfiguration.EnableInstallers();

            // Store information in memory for this example, rather than in
            // a database. In this sample, only subscription information is stored
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseContainer<WindsorBuilder>(c=>c.ExistingContainer(Container));
            endpointConfiguration.Conventions().DefiningEventsAs(t => t.Namespace.Contains("Events"));
                
            // Initialize the endpoint with the finished configuration
            endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Container.Register(Component.For<IMessageSession>().Instance(endpointInstance));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NUnit.Framework;
using OrderService.Client.Controllers;
using OrderService.Client.Database;

namespace OrderService.Client.Test
{
    [TestFixture]
    public class TestOrderApi
    {
        private IWindsorContainer Container { get; set; } = new WindsorContainer();

        public TestOrderApi()
        {
            Container.Install(FromAssembly.This());
        }
        //[Test]
        //public async Task TestOrderGet()
        //{
        //    var dbContext = Container.Resolve<IOrderServiceDbContext>();
        //    var controller = new OrderController(dbContext);
        //    var data = await controller.GetAllOrder() as OkNegotiatedContentResult<List<OrderModel>>;
        //    Assert.That(data, Is.Not.Null);
        //    Assert.That(data.Content.Count, Is.EqualTo(2));
        //}

        //[Test]
        //public async Task TestOrderAdd()
        //{
        //    var dbContext = Container.Resolve<IOrderServiceDbContext>();
        //    var controller  = new OrderController(dbContext);
        //    var data =
        //        await controller.AddNewOrder(new OrderModel {Quantity = 50, ProductId = 100}) as
        //            OkNegotiatedContentResult<OrderModel>;
        //    Assert.That(data, Is.Not.Null);
        //    Assert.That(data.Content.ProductId, Is.EqualTo(100));

        //}
    }

}


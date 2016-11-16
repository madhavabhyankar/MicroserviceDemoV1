using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using NServiceBus;
using Order.Contracts.Events;
using OrderService.Client.Database;
using Order = OrderService.Client.Database.Order;

namespace OrderService.Client.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private readonly IOrderServiceDbContext _context;
        private readonly IMessageSession _messageSession;

        public OrderController(IOrderServiceDbContext context, IMessageSession messageSession)
        {
            _context = context;
            _messageSession = messageSession;
        }
        [Route("getAllOrders")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllOrder()
        {
            var myOrders = new List<OrderModel>
            {
                new OrderModel {ProductId = 10, Quantity = 50},
                new OrderModel {ProductId = 50, Quantity = 100}
            };
            return Ok(myOrders);
        }

        
        [Route("addOrder")]
        [HttpPost]
        public async Task<IHttpActionResult> AddNewOrder(OrderModel orderModel)
        {
            _context.Orders.Add(new Database.Order {ProductId = orderModel.ProductId, Quantity = orderModel.Quantity});
            await _context.SaveChangesAsync();
            await _messageSession.Publish<NewOrder>(t =>
             {
                 t.Quantity = orderModel.Quantity;
                 t.ProductId = orderModel.ProductId;
             });
            return Ok(orderModel);
        }
    }
}

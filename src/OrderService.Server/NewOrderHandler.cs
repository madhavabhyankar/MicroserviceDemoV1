using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Order.Contracts.Events;

namespace OrderService.Server
{
    public class NewOrderHandler : IHandleMessages<NewOrder>
    {
        public Task Handle(NewOrder message, IMessageHandlerContext context)
        {
            Console.WriteLine($"New order received By the server Quantity: {message.Quantity}, Product: {message.ProductId}");
            return Task.FromResult(1);
        }
    }
}

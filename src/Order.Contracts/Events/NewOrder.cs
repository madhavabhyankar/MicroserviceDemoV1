using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Contracts.Events
{
    public class NewOrder
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}

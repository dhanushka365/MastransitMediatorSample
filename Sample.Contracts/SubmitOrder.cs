using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Contracts
{
    public class SubmitOrder
    {
        public Guid OrderId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? CustomerNumber { get; set; }
        public string? PaymentCardNumber { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
    }
}

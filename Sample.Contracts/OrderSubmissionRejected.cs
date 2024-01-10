using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Contracts
{
    public interface OrderSubmissionRejected
    {
        Guid OrderId { get; set; }
        DateTime Timestamp { get; set; }
        string? CustomerNumber { get; set; }
        string? Reason { get; set; }
    }
}

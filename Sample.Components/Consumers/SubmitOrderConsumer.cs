using MassTransit;
using Microsoft.Extensions.Logging;
using Sample.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Components.Consumers
{
    public class SubmitOrderConsumer :IConsumer<SubmitOrder>
    {
        readonly ILogger<SubmitOrderConsumer> _logger;
        public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
        {

            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            _logger.Log(LogLevel.Debug, "SubmitOrderConsumer: {CustomerNumber}", context.Message.CustomerNumber);

            if (context.Message.CustomerNumber.Contains("TEST"))
            {
                await context.RespondAsync<OrderSubmissionRejected>(new
                {
                    InVar.Timestamp,
                    context.Message.OrderId,
                    context.Message.CustomerNumber,
                    Reason = $"Test Customer cannot submit orders: {context.Message.CustomerNumber}"
                });
                return;
            }

            await context.RespondAsync<OrderSubmissionAccepted>(new
            {
                OrderId = context.Message.OrderId,
                Timestamp = InVar.Timestamp,
                CustomerNumber = context.Message.CustomerNumber
            });
        }

    }
}

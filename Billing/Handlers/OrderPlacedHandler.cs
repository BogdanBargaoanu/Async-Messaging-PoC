using Billing.Messages.Events;
using Microsoft.Extensions.Logging;
using Sales.Messages.Events;

namespace Billing.Handlers
{
    public class OrderPlacedHandler(ILogger<OrderPlacedHandler> logger) : IHandleMessages<OrderPlaced>
    {
        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            logger.LogInformation("Received OrderPlaced, charging OrderId: {orderId}", message.OrderId);

            // Business logic
            Thread.Sleep(200);

            var orderBilled = new OrderBilled
            {
                OrderId = message.OrderId
            };
            await context.Publish(orderBilled);
        }
    }
}

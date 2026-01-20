using Microsoft.Extensions.Logging;
using Sales.Messages.Commands;
using Sales.Messages.Events;

namespace Sales.Handlers
{
    public class PlaceOrderHandler(ILogger<PlaceOrderHandler> logger) : IHandleMessages<PlaceOrder>
    {
        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            logger.LogInformation("Received PlaceOrder, OrderId = {orderId}", message.OrderId);

            // Business logic
            Thread.Sleep(200);

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.OrderId
            };
            await context.Publish(orderPlaced);
        }
    }
}

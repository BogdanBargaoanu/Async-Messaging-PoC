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
            Thread.Sleep(100);

            var random = new Random().Next();
            if (random > 0.7 * int.MaxValue)
            {
                logger.LogError("Failed to place order, OrderId = {orderId}", message.OrderId);
                throw new Exception("Random failure occurred while placing order.");
            }

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.OrderId
            };
            await context.Publish(orderPlaced);
        }
    }
}

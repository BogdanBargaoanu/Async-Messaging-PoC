using Microsoft.Extensions.Logging;
using Sales.Messages.Commands;

namespace Sales.Handlers
{
    public class PlaceOrderHandler(ILogger<PlaceOrderHandler> logger) : IHandleMessages<PlaceOrder>
    {
        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            logger.LogInformation("Received PlaceOrder, OrderId = {orderId}", message.OrderId);
        }
    }
}

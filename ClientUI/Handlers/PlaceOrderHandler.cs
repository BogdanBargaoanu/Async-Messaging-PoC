using Messages.Commands;
using Microsoft.Extensions.Logging;

namespace ClientUI.Handlers
{
    public class PlaceOrderHandler(ILogger<PlaceOrderHandler> logger) : IHandleMessages<PlaceOrder>
    {
        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            logger.LogInformation("Received PlaceOrder, OrderId = {orderId}", message.OrderId);
        }
    }
}

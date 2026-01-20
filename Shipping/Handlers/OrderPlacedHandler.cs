using Microsoft.Extensions.Logging;
using Sales.Messages.Events;

namespace Shipping.Handlers
{
    public class OrderPlacedHandler(ILogger<OrderPlacedHandler> logger)
    //: IHandleMessages<OrderPlaced>
    {
        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            logger.LogInformation("Received OrderPlaced, OrderId: {orderId} - Should we ship?", message.OrderId);

        }
    }
}

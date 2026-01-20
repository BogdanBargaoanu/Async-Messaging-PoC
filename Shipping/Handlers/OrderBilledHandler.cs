using Billing.Messages.Events;
using Microsoft.Extensions.Logging;

namespace Shipping.Handlers
{
    public class OrderBilledHandler(ILogger<OrderBilledHandler> logger)
    //: IHandleMessages<OrderBilled>
    {
        public async Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            logger.LogInformation("Received OrderBilled, OrderId: {orderId} - Should we ship?", message.OrderId);

        }
    }
}

using Billing.Messages.Events;
using Microsoft.Extensions.Logging;
using Sales.Messages.Events;

namespace Shipping.Policies.Shipping
{
    public class ShippingPolicy(ILogger<ShippingPolicy> logger) : Saga<SagaData>,
        IAmStartedByMessages<OrderPlaced>,
        IAmStartedByMessages<OrderBilled>,
        IHandleTimeouts<OrderNotBilledTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.MapSaga(sd => sd.OrderId)
                .ToMessage<OrderPlaced>(msg => msg.OrderId)
                .ToMessage<OrderBilled>(msg => msg.OrderId);
        }

        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            this.Data.Placed = true;
            logger.LogInformation("Received OrderPlaced, OrderId = {orderId} - Status: {placed}, {billed}", message.OrderId, this.Data.Placed, this.Data.Billed);

            await ProcessOrder(context);

            if (!Data.Billed)
            {
                await this.RequestTimeout<OrderNotBilledTimeout>(context, TimeSpan.FromSeconds(5));
            }
        }

        public async Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            this.Data.Billed = true;
            logger.LogInformation("Received OrderBilled, OrderId = {orderId} - Status: {placed}, {billed}", message.OrderId, this.Data.Placed, this.Data.Billed);

            await ProcessOrder(context);
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (this.Data.Placed && this.Data.Billed)
            {
                logger.LogInformation("Shipping order, OrderId = {orderId}", this.Data.OrderId);
                MarkAsComplete();
            }
        }

        public async Task Timeout(OrderNotBilledTimeout state, IMessageHandlerContext context)
        {
            logger.LogInformation("Order not billed, OrderId = {orderId}", this.Data.OrderId);
        }
    }
}

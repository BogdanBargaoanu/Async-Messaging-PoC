using Billing.Messages.Events;
using Microsoft.Extensions.Logging;
using Sales.Messages.Events;

namespace Shipping.Policies
{
    public class ShippingPolicy(ILogger<ShippingPolicy> logger) : Saga<SagaData>,
        IAmStartedByMessages<OrderPlaced>,
        IAmStartedByMessages<OrderBilled>
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
    }

    public class SagaData : ContainSagaData
    {
        public string OrderId { get; set; }
        public bool Placed { get; set; }
        public bool Billed { get; set; }
    }
}
